# supexlms
SUPEX LMS

1. Pre-requisite
    - MySQL database
   
2. Installation Guide
    2.1. Database installation
        1. Install MySQL 5.6 or higher version
        2. Set root password as "password!01" when you install MySQL. If you already have MySQL, change the crendential in Web.Config
        3. From Visual Studio, go to TOOLS > NuGet Package Manager > Package Manager Console
        4. Change Default project into Lms.Domain
        5. type enable-migrations and run.
        6. type update-database and run. This command will create the all database object automatically.
   
   
3. Lauch the web application
    1. Set the Lms.WebApp project as a start-up project.
    2. Change web.config according to your workstation.
        - LmsWebPath: Multi-tanancy website root directory. This path SHOULD be the directory path the Lms.LmsWeb project resides.
        - LmsWebVirDir: Resources like image, SCORM file will locate. All resource will be shared with multi-tenancy website
    3. Once you lanch the project, you will see the .NET basic UI page.
    4. Open Trial page at the top, and put your information.
    5. After creating the website, the system will be redirected to your LMS page. This main domain configuration comes from Web.config.
       However, you will see error page because your LMS site is not running yet.
       Actually, the trial request process will not work for IIS Express. Above steps are to create basic database information.
    6. Once you create the test website, you don't need to do the above process for development.       
    7. Open MySQL database and run this query.
       UPDATE lms.company SET HostName = 'localhost';
    8. Set the Lms.LmsWeb project as a start-up project, and lauch the site
    9. Use credentail you used step 4.
    