using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Courses
{
    public class LessonServiceImpl : ILessonService
    {
        protected IUnitOfWork unitOfWork;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public LessonServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void SetScormData(string userId, string lessonId, string enrollmentId, string param, string value)
        {
            var enrollment = unitOfWork.EnrollmentRepository.GetById(enrollmentId);

            if (enrollment.UserId == userId)
            {
                var scormData = enrollment.ScormData.SingleOrDefault(x => x.LessonId == lessonId);
                if (scormData != null)
                {
                    JObject jsonData = null;

                    if (scormData.DataResult == ScormDataResultEnum.NotStarted)
                        scormData.DataResult = ScormDataResultEnum.Started;

                    if (scormData.TemporaryData != null)
                    {
                        jsonData = JObject.Parse(scormData.TemporaryData);

                        if (jsonData[param] == null)
                            jsonData.Add(new JProperty(param, value));
                        else
                            jsonData[param] = value;

                        scormData.TemporaryData = jsonData.ToString();
                    }
                    else
                    {
                        jsonData = new JObject(new JProperty(param, value));
                        scormData.TemporaryData = jsonData.ToString();
                    }

                    unitOfWork.EnrollmentRepository.Update(enrollment);
                    unitOfWork.SaveChanges();
                }
                else
                {
                    scormData = new ScormData();
                    JObject jsonData = new JObject();
                    scormData.DataResult = ScormDataResultEnum.Started;
                    jsonData.Add(new JProperty(param, value));
                    scormData.TemporaryData = jsonData.ToString();
                    enrollment.ScormData.Add(scormData);

                    unitOfWork.EnrollmentRepository.Update(enrollment);
                    unitOfWork.SaveChanges();
                }
            }
        }

        public string GetScormData(string userId, string lessonId, string enrollmentId, string param)
        {
            var enrollment = unitOfWork.EnrollmentRepository.GetById(enrollmentId);

            if (enrollment.UserId == userId)
            {
                var scormData = enrollment.ScormData.SingleOrDefault(x => x.LessonId == lessonId);

                if (scormData != null)
                {
                    JObject jsonData = JObject.Parse(scormData.TemporaryData);
                    var result = jsonData[param];
                    if (result != null)
                        return result.ToString();
                }
            }


            return null;
        }

        public void CommitScormData(string userId, string lessonId, string enrollmentId)
        {
            var enrollment = unitOfWork.EnrollmentRepository.GetById(enrollmentId);

            if (enrollment.UserId == userId)
            {
                var scormData = enrollment.ScormData.SingleOrDefault(x => x.LessonId == lessonId);

                if (scormData != null && !scormData.IsCompleted)
                {
                    if (scormData.PersistentData == null)
                    {
                        scormData.PersistentData = scormData.TemporaryData;
                    }
                    else
                    {
                        var tempData = JObject.Parse(scormData.TemporaryData);
                        var persistData = JObject.Parse(scormData.PersistentData);

                        foreach (var x in tempData)
                        { // if 'obj' is a JObject
                            string param = x.Key;
                            string value = x.Value.ToString();
                            logger.Debug("name: {0}, value: {1}", param, value);


                            if (persistData[param] == null)
                            {
                                persistData.Add(new JProperty(param, value));
                            }
                            else
                            {
                                persistData[param] = value;
                            }
                        }

                        scormData.PersistentData = persistData.ToString();
                    }

                    logger.Trace("CheckLessonCompletion calling...");
                    CheckScormCompletion(scormData);
                    CheckCourseCompletion(enrollment, scormData.Lesson);

                    unitOfWork.EnrollmentRepository.Update(enrollment);
                    unitOfWork.SaveChanges();
                }


            }
        }




        private void CheckCourseCompletion(Enrollment enrollment, Lesson lesson)
        {
            
            int lessonNum = enrollment.ScormData.Count() + enrollment.QuizData.Count();
            int completedNum = enrollment.ScormData.Count(x => x.IsCompleted) + enrollment.QuizData.Count(x => x.IsCompleted);

            logger.Info("userId: {0}, lessonNum: {1}, completedNum: {2}, incompletedNum: {3}",
                enrollment.UserId, lessonNum, completedNum, (lessonNum - completedNum));


            if (lessonNum == completedNum)
            {
                enrollment.SetEnrollmentResult(EnrollResultEnum.Passed);
                enrollment.User.AssignCertificate(lesson.Course.Certificate);                
            }
        }


        private void CheckScormCompletion(ScormData scormData)
        {
            logger.Trace("Inside CheckLessonCompletion");

            var jsonData = JObject.Parse(scormData.PersistentData);
            var lessonStatus = jsonData["cmi.core.lesson_status"];
            var successStatus = jsonData["cmi.success_status"];

            logger.Info("id: {0}, lessonStatus: {1}, successStatus: {2}", scormData.Id, lessonStatus, successStatus);

            if ((lessonStatus != null && lessonStatus.ToString() == "passed") || (successStatus != null && successStatus.ToString() == "passed"))
            {
                scormData.IsCompleted = true;
                scormData.DataResult = ScormDataResultEnum.Passed;

                logger.Debug("Passed... lessonData.DataResult: " + scormData.DataResult);
            }

            if ((lessonStatus != null && lessonStatus.ToString() == "failed") || (successStatus != null && successStatus.ToString() == "failed"))
            {
                scormData.IsCompleted = true;
                scormData.DataResult = ScormDataResultEnum.Faild;
            }
        }

        public void Grade(string userId, string quizId, string lessonId, string enrollmentId, List<UserAnswer> userAnswers)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            var enrollment = unitOfWork.EnrollmentRepository.GetById(enrollmentId);
            

            float passPercent = quiz.PassPercent;
            int totalQuizNum = quiz.LoadActiveQuizQuestions().Count();
            int rightAnswerNum = 0, wrongAnswerNum = 0;
            string questionId = string.Empty;
            QuizQuestion quizQuestion = null;

            foreach (UserAnswer userAnswer in userAnswers)
            {
                questionId = userAnswer.QuizQuestionId;
                quizQuestion = quiz.QuizQuestions.Single(x => x.Id == questionId);

                switch (quizQuestion.Type)
                {
                    case QuizQuestionTypeEnum.Keyword:
                        if (HasRightAnswerForKeyword(userAnswer.QuizUserAnswer, quizQuestion.QuizAnswers.First(x => x.IsRight).Title))
                            rightAnswerNum++;
                        else
                            wrongAnswerNum++;
                        break;
                    case QuizQuestionTypeEnum.MultiSelect:
                        var multiSelectAnswer = userAnswer.QuizUserAnswer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (HasRightAnswerForMultiSelect(multiSelectAnswer, quizQuestion.QuizAnswers.Where(x => x.IsRight).Select(x => x.Id).ToList()))
                            rightAnswerNum++;
                        else
                            wrongAnswerNum++;
                        break;
                    case QuizQuestionTypeEnum.SingleSelect:
                        if (userAnswer.QuizUserAnswer == quizQuestion.QuizAnswers.First(x => x.IsRight).Id)
                            rightAnswerNum++;
                        else
                            wrongAnswerNum++;
                        break;
                }
            }

            float score = (rightAnswerNum * 100f) / totalQuizNum;

            logger.Info("totalQuizNum: {0}, rightAnswer: {1}, wrongAnswer: {2}", totalQuizNum, rightAnswerNum, wrongAnswerNum);
            logger.Info("userId: {0}, quizId: {1}, score: {2}", enrollment.UserId, quizId, score);

            var quizData = new QuizData();
            quizData.LessonId = lessonId;
            quizData.IsCompleted = true;
            quizData.DataResult = score >= passPercent ? QuizDataResultEnum.Passed : QuizDataResultEnum.Faild;
            quizData.PersistentData = JsonConvert.SerializeObject(userAnswers);
            quizData.Score = score;
            enrollment.QuizData.Add(quizData);

            CheckCourseCompletion(enrollment, quizData.Lesson);

            unitOfWork.EnrollmentRepository.Update(enrollment);
            unitOfWork.SaveChanges();
        }

        private bool HasRightAnswerForKeyword(string userAnswer, string rightAnswer)
        {
            return userAnswer.Trim().ToUpper() == rightAnswer.Trim().ToUpper();
        }

        private bool HasRightAnswerForMultiSelect(List<string> userAnswers, List<string> rightAnswers)
        {
            if (userAnswers.Count == rightAnswers.Count)
            {
                return userAnswers.OrderBy(x => x).SequenceEqual(rightAnswers.OrderBy(x => x));
            }

            return false;
        }




        public void CreateScormLesson(string companyId, string updaterId, string courseId, string scormId, string name, string description)
        {
            logger.Info("[user: {0}] creates a scorm lesson [courseId: {1}, scormId: {2}]",
                updaterId, courseId, scormId);

            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                course.AddScormLesson(scormId, name, description);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public void CreateQuizLesson(string companyId, string updaterId, string courseId, string quizId, string name, string description)
        {
            logger.Info("[user: {0}] creates a quiz lesson [courseId: {1}, quizId: {2}]",
                updaterId, courseId, quizId);

            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                course.AddQuizLesson(quizId, name, description);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public void CreateExternalLesson(string companyId, string updaterId, string courseId, string name, string description)
        {
            logger.Info("[user: {0}] creates a external lesson [courseId: {1}]",
                updaterId, courseId);

            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                course.AddExternalLesson(name, description);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public void DeleteLesson(string companyId, string updaterId, string courseId, string lessonId)
        {
            logger.Info("[user: {0}] deletes a lesson [courseId: {1}, lessonId: {2}]", updaterId, courseId, lessonId);
            var course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.Company.Id == companyId)
            {
                var lesson = course.Lessons.SingleOrDefault(x => x.Id == lessonId);
                if (lesson != null)
                {
                    lesson.IsDeleted = true;
                    lesson.UpdatedTs = DateTime.UtcNow;

                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.SaveChanges();
                }
            }
        }

        public Lesson GetLessonById(string lessonId, string enrollmentId)
        {
            var enrollment = unitOfWork.EnrollmentRepository.GetById(enrollmentId);
            return enrollment.Session.Course.Lessons.SingleOrDefault(x => x.Id == lessonId);
        }

        public Lesson GetLessonById(string companyId, string courseId, string lessonId)
        {
            var lesson = unitOfWork.CourseRepository.GetById(courseId).Lessons.SingleOrDefault(x => x.Id == lessonId && !x.IsDeleted);
            if (lesson.Course.CompanyId == companyId)
                return lesson;
            else
                return null;
        }

        public void EditScormLesson(string companyId, string updaterId, string courseId, string lessonId, string scormId, string name, string description)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);
            
            if (course.CompanyId == companyId)
            {
                var lesson = course.Lessons.SingleOrDefault(x => x.Id == lessonId && !x.IsDeleted);

                if (lesson != null)
                {
                    lesson.Name = name;
                    lesson.Description = description;
                    lesson.ScormId = null;
                    lesson.QuizId = null;
                    lesson.ScormId = scormId;
                    lesson.UpdatedTs = DateTime.UtcNow;

                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.SaveChanges();
                }
            }   
        }

        public void EditQuizLesson(string companyId, string updaterId, string courseId, string lessonId, string quizId, string name, string description)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                var lesson = course.Lessons.SingleOrDefault(x => x.Id == lessonId && !x.IsDeleted);

                if (lesson != null)
                {
                    lesson.Name = name;
                    lesson.Description = description;
                    lesson.ScormId = null;
                    lesson.QuizId = null;
                    lesson.QuizId = quizId;
                    lesson.UpdatedTs = DateTime.UtcNow;

                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.SaveChanges();
                }
            } 
        }

        public void EditExternalLesson(string companyId, string updaterId, string courseId, string lessonId, string name, string description)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);
            
            if (course.CompanyId == companyId)
            {
                var lesson = course.Lessons.SingleOrDefault(x => x.Id == lessonId && !x.IsDeleted);

                if (lesson != null)
                {
                    lesson.Name = name;
                    lesson.Description = description;
                    lesson.ScormId = null;
                    lesson.QuizId = null;
                    lesson.UpdatedTs = DateTime.UtcNow;

                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.SaveChanges();
                }
            } 
        }
    }
}
