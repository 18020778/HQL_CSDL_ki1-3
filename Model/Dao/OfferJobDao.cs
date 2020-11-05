using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OfferJobDao
    {
        CareerWeb db = null;
        public OfferJobDao()
        {
            db = new CareerWeb();
        }
        public List<OfferJob> ListByEmployee(String eplID)
        {
            return db.OfferJobs.Where(x => x.EmployeeID == eplID).OrderBy(x => x.OfferCreateDate).ToList();
        }
        public OfferJob FindByID(Guid offerID)
        {
            try
            {
                return db.OfferJobs.Find(offerID);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Insert(OfferJob of)
        {
            try
            {
                db.OfferJobs.Add(of);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Edit(OfferJob of)
        {
            try
            {
                var modifyJob = db.OfferJobs.Find(of.OfferID);
                modifyJob.OfferName = of.OfferName;
                if (of.OfferDescription != null)
                {
                    modifyJob.OfferDescription = of.OfferDescription;
                }
                modifyJob.OfferLimitDate = of.OfferLimitDate;
                if (of.OfferImage != null)
                {
                    modifyJob.OfferImage = of.OfferImage;
                }
                modifyJob.OfferMajor = of.OfferMajor;
                modifyJob.OfferPosition = of.OfferPosition;
                modifyJob.OfferSalary = of.OfferSalary;
                modifyJob.Sex = of.Sex;
                modifyJob.LearningLevelRequest = of.LearningLevelRequest;
                modifyJob.ExperienceRequest = of.ExperienceRequest;
                modifyJob.Amount = of.Amount;
                modifyJob.Area = of.Area;
                modifyJob.JobAddress = of.JobAddress;
                modifyJob.ContactEmail = of.ContactEmail;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOffer(Guid offerID)
        {
            try
            {
                var delOffer = db.OfferJobs.Find(offerID);
                db.OfferJobs.Remove(delOffer);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<OfferJob> ListOfferJob()
        {
            return db.OfferJobs.ToList();
        }
        public List<OfferJob> ReturnFilterList(string offerName = "0", int Area = 0,
            int OfferMajor = 0, int offerSalary = 0, int positionJobId = 0,
            string sex = "0", int experienceRequest = 0, int learningLevelRequest = 0)
        {

            try
            {
                var listOfferJob = new OfferJobDao().ListOfferJob();
                var listOfferJobMajor = new OfferJobSkillDao().ReturnList();
                var listEnterprise = new EnterpriseDao().ReturnList();
                var result = (from job in listOfferJob
                              join jobMajor in listOfferJobMajor on job.OfferID equals jobMajor.OfferID
                              join enterprise in listEnterprise on job.EnterpriseID equals enterprise.EnterpriseID
                              where (Area == 0 || job.Area == Area)
                              && (OfferMajor == 0 || job.OfferMajor == OfferMajor)
                              && (offerSalary == 0 || job.OfferSalary == offerSalary)
                              && (positionJobId == 0 || job.OfferPosition == positionJobId)
                              && (sex == "0" || job.Sex == sex)
                              && (experienceRequest == 0 || job.ExperienceRequest == experienceRequest)
                              && (learningLevelRequest == 0 || job.LearningLevelRequest == learningLevelRequest)
                              && (offerName == "0" || job.OfferName.Contains(offerName) || enterprise.EnterpriseName.Contains(offerName))


                              select new
                              {
                                  OfferID = job.OfferID,
                                  OfferName = job.OfferName,
                                  EnterpriseID = job.EnterpriseID,
                                  EnterpriseName = db.Enterprises.Find(job.EnterpriseID).EnterpriseName,
                                  JobAddress = job.JobAddress,
                                  OfferSalary = job.OfferSalary,
                                  Amount = db.Salaries.Find(job.OfferSalary).Amount,
                                  OfferLimitDate = job.OfferLimitDate,
                                  Bonus = job.Bonus,
                                  OfferImage = job.OfferImage,

                              }).AsEnumerable().Select(x => new OfferJob()
                              {
                                  OfferID = x.OfferID,
                                  OfferName = x.OfferName,
                                  EnterpriseID = x.EnterpriseID,
                                  JobAddress = x.JobAddress,
                                  OfferSalary = x.OfferSalary,
                                  OfferLimitDate = x.OfferLimitDate,
                                  Bonus = x.Bonus,
                                  OfferImage = x.OfferImage
                              });

                List<OfferJob> finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                List<OfferJob> finalResult2 = new List<OfferJob>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].OfferID == finalResult2[j].OfferID)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        finalResult2.Add(finalResult[i]);
                    }
                }
                return finalResult2;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<OfferJob> ShowContainer(Guid EnterpriseID)
        {
            return db.OfferJobs.Where(x => x.EnterpriseID == EnterpriseID).ToList();
        }

        public List<ShowFullJob> ShowDetail(Guid OfferID)
        {
            var listOfferJob = new OfferJobDao().ListOfferJob();
            var listEnterprise = new EnterpriseDao().ReturnList();
            var result = (from job in listOfferJob
                          join enpr in listEnterprise on job.EnterpriseID equals enpr.EnterpriseID
                          where job.OfferID == OfferID
                          select new
                          {
                              EnterpriseID = job.EnterpriseID,
                              OfferName = job.OfferName,
                              OfferDescription = job.OfferDescription,
                              JobAddress = job.JobAddress,
                              Amount = db.Salaries.Find(job.OfferSalary).Amount,
                              Bonus = job.Bonus,
                              AmountNum = job.Amount,
                              Sex = job.Sex,
                              NameArea = db.Areas.Find(job.Area).NameArea,
                              OfferLimitDate = job.OfferLimitDate,
                              Applications = job.Applications,
                              Time = db.Experiences.Find(job.ExperienceRequest).Time,
                              NameLevel = db.LevelLearnings.Find(job.LearningLevelRequest).NameLevel,
                              NamePosition = db.PositionEmployees.Find(job.OfferPosition).NamePosition,
                              EnterpriseName = enpr.EnterpriseName,
                              AmountSize = db.EnterpriseSizes.Find(enpr.EnterpriseSize).AmountSize,
                              ImageLogo = enpr.ImageLogo,
                              NameOfEnterprise = db.TypeOfEnterprises.Find(enpr.TypeOfEnterprise).NameOfEnterprise,
                              listJobId = db.EnterpriseJobs.Where(x => x.EnterpriseID == enpr.EnterpriseID).Select(x => x.JobId).ToList(),

                          }).AsEnumerable().Select(x => new ShowFullJob()
                          {
                              EnterpriseID = x.EnterpriseID,
                              OfferName = x.OfferName,
                              OfferDescription = x.OfferDescription,
                              JobAddress = x.JobAddress,
                              Amount = x.Amount,
                              Bonus = x.Bonus,
                              AmountNum = x.AmountNum,
                              Sex = x.Sex,
                              NameArea = x.NameArea,
                              OfferLimitDate = x.OfferLimitDate,
                              Applications = x.Applications,
                              Time = x.Time,
                              NameLevel = x.NameLevel,
                              NamePosition = x.NamePosition,
                              EnterpriseName = x.EnterpriseName,
                              AmountSize = x.AmountSize,
                              ImageLogo = x.ImageLogo,
                              NameOfEnterprise = x.NameOfEnterprise,
                              listJobId = x.listJobId,
                          });
            var finalResult = result.ToList();
            return finalResult;
        }
    }
}
