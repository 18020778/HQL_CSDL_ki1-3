using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class UserDao
    {
        CareerWeb db = null;
        public UserDao()
        {
            db = new CareerWeb();
        }
        public bool Delete(String id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public User FindById(string UserId)
        {
            try
            {
                var userAcc = db.Users.SingleOrDefault(x => x.UserId == UserId);
                return userAcc;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool ModifyUserBasic(String userId, ModifyUserForm user)
        {
            try
            {
                var userModify = db.Users.Find(userId);
                userModify.UserName = user.userName;
                userModify.UserBirthDay = DateTime.ParseExact(user.userDob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                userModify.UserEmail = user.userEmail;
                userModify.UserArea = user.userArea;
                userModify.UserMobile = user.userMobile;
                userModify.Sex = user.userGender;
                if(user.userAddress != null && user.userAddress != "")
                {
                    userModify.UserAddress = user.userAddress;
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool ModifyUserJob(String userId, User user)
        {
            try
            {
                var modifyUser = db.Users.Find(userId);
                modifyUser.DesiredJob = user.DesiredJob;
                modifyUser.PositionApply = user.PositionApply;
                modifyUser.Salary = user.Salary;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool UploadImage(string userId, string fileName)
        {
            try
            {
                var user = db.Users.Find(userId);
                user.UserImage = fileName;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool UploadVideo(string userId, string fileName)
        {
            try
            {
                var user = db.Users.Find(userId);
                user.VideoProfile = fileName;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //public List<CandidateInfo> ListUserFit(String name, int areaID, int JobId)
        //{
        //    var listUserMajors = new UserMajorDao().ListUserMajor();
        //    var listJobs = new JobMajorDao().ListJobMain();

        //    var result = (from User in db.Users.ToList()
        //                  join UserMajor in listUserMajors on User.UserId equals UserMajor.UserID
        //                  where (areaID == 0 || User.UserArea == areaID)
        //                  where (JobId == 0 || UserMajor.MajorID == JobId)
        //                  where (name == "0" || User.UserName.Contains(name))
        //                  select new
        //                  {
        //                      UserId = User.UserId,
        //                      UserName = User.UserName,
        //                      //UserImage = User.UserImage,
        //                      //UserExperience = User.UserExperience,
        //                      //UserSalary = User.GPA,
        //                      UserArea = db.Areas.Find(User.UserArea).NameArea,
        //                      UserMajorName = (from UserMajor in listUserMajors
        //                                       join JobMajor in listJobs on UserMajor.MajorID equals JobMajor.JobID

        //                                       where (UserMajor.UserID == User.UserId)

        //                                       select JobMajor.JobName).ToList()

        //                  }).AsEnumerable().Select(x => new CandidateInfo()
        //                  {
        //                      UserId = x.UserId,
        //                      UserName = x.UserName,
        //                      //UserImage = x.UserImage,
        //                      //UserExperience = x.UserExperience,
        //                      //UserSalary = (float)x.UserSalary,
        //                      UserArea = x.UserArea,
        //                      UserMajorName = x.UserMajorName
        //                  });

        //    List<CandidateInfo> finalResult = result.ToList();
        //    int n = finalResult.Count;
        //    if (n == 0 || n == 1) return finalResult;

        //    List<CandidateInfo> finalResult2 = new List<CandidateInfo>();

        //    for (int i = 0; i < n; i++)
        //    {
        //        bool check = true;
        //        for (int j = 0; j < finalResult2.Count; j++)
        //        {
        //            if (finalResult[i].UserId == finalResult2[j].UserId)
        //            {
        //                check = false;
        //                break;
        //            }
        //        }
        //        if (check == true)
        //        {
        //            finalResult2.Add(finalResult[i]);
        //        }


        //    }
        //    return finalResult2;


        //}
        public List<CandidateInfo> ListUserFit(string KeyWord, int AreaID, int JobID, int experienceID, int salaryID, int positionID, int levelLearningID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@name", KeyWord),
                    new SqlParameter("@areaID", AreaID),
                    new SqlParameter("@jobID", JobID),
                    new SqlParameter("@experienceID", experienceID),
                    new SqlParameter("@salaryID", salaryID),
                    new SqlParameter("@positionID", positionID),
                    new SqlParameter("@levelLearningID", levelLearningID)
                };
                var data = db.Database.SqlQuery<CandidateInfo>("sp_User_listUserFit @name,@areaID,@jobID,@experienceID,@salaryID,@positionID,@levelLearningID", param).ToList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //    var listUsers = ListUser();
        //    var listUserMajors = new UserMajorDao().ListUserMajor();
        //    var listJobs = new JobMajorDao().ListJobMain();
        //    var listLanguage = new UserForeignLanguageDao().ReturnList();

        //    var result = (from User in listUsers
        //                  join UserMajor in listUserMajors on User.UserId equals UserMajor.UserID
        //                  where (AreaID == 0 || User.UserArea == AreaID)
        //                  && (JobID == 0 || UserMajor.MajorID == JobID)
        //                  && (salaryID == 0 || User.Salary == salaryID)
        //                  && (KeyWord == "" || User.UserName.Contains(KeyWord))
        //                  select new
        //                  {
        //                      UserId = User.UserId,
        //                      UserName = User.UserName,
        //                      //UserImage = User.UserImage,
        //                      //UserExperience = User.UserExperience,
        //                      UserSalary = (User.Salary != null) ? User.Salary : 0,
        //                      UserArea = db.Areas.Find(User.UserArea).NameArea,
        //                      UserMajorName = (from UserMajor in listUserMajors
        //                                       join JobMajor in listJobs on UserMajor.MajorID equals JobMajor.JobID

        //                                       where (UserMajor.UserID == User.UserId)

        //                                       select JobMajor.JobName).ToList()

        //                  }).AsEnumerable().Select(x => new CandidateInfo()
        //                  {
        //                      UserId = x.UserId,
        //                      UserName = x.UserName,
        //                      //UserImage = x.UserImage,
        //                      //UserExperience = x.UserExperience,
        //                      UserSalary = new SalaryDao().AmountSalary(x.UserSalary.Value),
        //                      UserArea = x.UserArea,
        //                      UserMajorName = x.UserMajorName
        //                  });

        //    List<CandidateInfo> finalResult = result.ToList();
        //    int n = finalResult.Count;
        //    if (n == 0 || n == 1) return finalResult;

        //    List<CandidateInfo> finalResult2 = new List<CandidateInfo>();

        //    for (int i = 0; i < n; i++)
        //    {
        //        bool check = true;
        //        for (int j = 0; j < finalResult2.Count; j++)
        //        {
        //            if (finalResult[i].UserId == finalResult2[j].UserId)
        //            {
        //                check = false;
        //                break;
        //            }
        //        }
        //        if (check == true)
        //        {
        //            finalResult2.Add(finalResult[i]);
        //        }


        //    }
        //    return finalResult2;


        //}

        public List<User> ListUser()
        {
            return db.Users.ToList();
        }
        public List<User> ListUsers()
        {
            try
            {
                return db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<ShowInfoCandidate> InfoUser(string UserID)
        {
            var listUser = new UserDao().ListUsers();
            var listMajor = new UserMajorDao().ListUserMajor();
            var listStudy = new UserLearningDao().ListUserLearning();

            var result = (from user in listUser
                          join major in listMajor on user.UserId equals major.UserID
                          join study in listStudy on user.UserId equals study.UserID
                          where user.UserId == UserID
                          select new
                          {
                              UserName = user.UserName,
                              UserImage = user.UserImage,
                              UserBirthDay = user.UserBirthDay,
                              UserEmail = user.UserEmail,
                              UserAddress = user.UserAddress,
                              UserMobile = user.UserMobile,
                              listJob = db.JobMajors.Where(x => x.JobID == major.MajorID).Select(x => x.JobName).ToList(),
                              Amount = db.Salaries.Find(user.Salary).Amount,
                              NamePosition = db.PositionEmployees.Find(user.PositionApply).NamePosition,
                              UserArea = db.Areas.Find(user.UserArea).NameArea,
                              StudyLevel = db.LevelLearnings.Find(user.LevelLearning).NameLevel,
                              SchoolName = study.SchoolName,
                              TimeStart = study.TimeStart,
                              TimeEnd = study.TimeEnd,
                              JobName = db.JobMajors.Find(study.Major).JobName,
                          }).AsEnumerable().Select(x => new ShowInfoCandidate()
                          {
                              UserName = x.UserName,
                              UserImage = x.UserImage,
                              UserBirthDay = x.UserBirthDay,
                              UserEmail = x.UserEmail,
                              UserAddress = x.UserAddress,
                              UserArea = x.UserArea,
                              UserMobile = x.UserMobile,
                              listJob = x.listJob,
                              Amount = x.Amount,
                              NamePosition = x.NamePosition,
                              StudyLevel = x.StudyLevel,
                              SchoolName = x.SchoolName,
                              TimeStart = x.TimeStart,
                              TimeEnd = x.TimeEnd,
                              JobName = x.JobName

                          });
            return result.ToList();
        }

    }
}
