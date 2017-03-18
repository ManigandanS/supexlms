using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Courses
{
    public interface ILessonService
    {
        void CreateScormLesson(string companyId, string updaterId, string courseId, string scormId, string name, string description);

        void CreateQuizLesson(string companyId, string updaterId, string courseId, string quizId, string name, string description);

        void CreateExternalLesson(string companyId, string updaterId, string courseId, string name, string description);

        void EditScormLesson(string companyId, string updaterId, string courseId, string lessonId, string scormId, string name, string description);

        void EditQuizLesson(string companyId, string updaterId, string courseId, string lessonId, string quizId, string name, string description);

        void EditExternalLesson(string companyId, string updaterId, string courseId, string lessonId, string name, string description);

        void DeleteLesson(string companyId, string updaterId, string courseId, string lessonId);

        Lesson GetLessonById(string lessonId, string enrollmentId);

        Lesson GetLessonById(string companyId, string courseId, string lessonId);

        void SetScormData(string userId, string lessonId, string enrollmentId, string param, string value);

        string GetScormData(string userId, string lessonId, string enrollmentId, string param);

        void CommitScormData(string userId, string lessonId, string enrollmentId);

        void Grade(string userId, string quizId, string lessonId, string enrollmentId, List<UserAnswer> userAnswers);
    }
}
