using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class EnterpriseDao
    {
        CareerWeb db = null;
        public EnterpriseDao()
        {
            db = new CareerWeb();
        }
        public string EnterpriseName(Guid id)
        {
            return db.Enterprises.Find(id).EnterpriseName;
        }
        public bool Insert(Enterprise ins)
        {
            try
            {
                db.Enterprises.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public Enterprise FindById(Guid id)
        {
            try
            {
                return db.Enterprises.Find(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Enterprise AddCode(Guid id, string code)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.Code = code;
                db.SaveChanges();
                return ent;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Enterprise> ListResponse()
        {
            try
            {
                return db.Enterprises.Where(x => x.Status == false).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool RemoveEnterprise(Guid id)
        {
            try
            {
                var rm = db.Enterprises.Find(id);
                db.Enterprises.Remove(rm);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool ChangeStatus(Guid id)
        {
            try
            {
                var cg = db.Enterprises.Find(id);
                cg.Status = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<Enterprise> ReturnList()
        {
            try
            {
                return db.Enterprises.ToList();
            } catch
            {
                return null;
            }
        }
        public List<FormEnterpriseFull> ReturnFilterList(string EName = "", int EArea = 0, int ECareer = 0, int ESize = 0)
        {
            try
            {
                //var listEnterprise = new EnterpriseDao().ReturnList();
                //var listArea = new EnterpriseAreaDao().ListEnterpriseArea();
                //var listJob = new EnterpriseJobDao().ListEnterpriseJob();
                //var result = (from listE in listEnterprise
                //              join listA in listArea on listE.EnterpriseID equals listA.EnterpriseId
                //              join listJ in listJob on listE.EnterpriseID equals listJ.EnterpriseID
                //              where (EArea == 0 || listA.AreaID == EArea)
                //              && (ECareer == 0 || listJ.JobId == ECareer)
                //              && (ESize == 0 || listE.EnterpriseSize == ESize)
                //              && (EName == "" || listE.EnterpriseName.Contains(EName))
                //              && listE.Status == true

                //              select new
                //              {
                //                  EnterpriseID = listE.EnterpriseID,
                //                  EnterpriseName = listE.EnterpriseName,
                //                  ImageLogo = listE.ImageLogo,
                //                  NameArea = db.Areas.Find(listA.AreaID).NameArea,
                //                  listJobId = db.EnterpriseJobs.Where(x => x.EnterpriseID == listE.EnterpriseID).Select(x => x.JobId).ToList(),

                //              }).AsEnumerable().Select(x => new FormEnterpriseFull()
                //              {
                //                  EnterpriseID = x.EnterpriseID,
                //                  EnterpriseName = x.EnterpriseName,
                //                  ImageLogo = x.ImageLogo,
                //                  NameArea = x.NameArea,
                //                  listJobId = x.listJobId
                //              });

                //var finalResult = result.ToList();
                //int n = finalResult.Count;
                //if (n == 0 || n == 1) return finalResult;

                //var finalResult2 = new List<FormEnterpriseFull>();

                //for (int i = 0; i < n; i++)
                //{
                //    bool check = true;
                //    for (int j = 0; j < finalResult2.Count; j++)
                //    {
                //        if (finalResult[i].EnterpriseID == finalResult2[j].EnterpriseID)
                //        {
                //            check = false;
                //            break;
                //        }
                //    }
                //    if (check == true)
                //    {
                //        finalResult2.Add(finalResult[i]);
                //    }
                //}
                //return finalResult2;
                SqlParameter[] param = new SqlParameter[]
               {
                    new SqlParameter("@name", EName),
                    new SqlParameter("@areaID", EArea),
                    new SqlParameter("@careerID",ECareer),
                    new SqlParameter("@sizeID", ESize)
               };
                var data = db.Database.SqlQuery<FormEnterpriseFull>("sp_Enterprise_Filter @name,@areaID,@careerID,@sizeID", param).ToList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<ShowFullEnterprise> ShowEnterprise(Guid EnterpriseID)
        {
            try
            {
                var listEnterprise = new EnterpriseDao().ReturnList();
                var listArea = new EnterpriseAreaDao().ListEnterpriseArea();
                var result = (from enpr in listEnterprise
                              join listA in listArea on enpr.EnterpriseID equals listA.EnterpriseId
                              where enpr.EnterpriseID == EnterpriseID
                              select new
                              {
                                  EnterpriseID = enpr.EnterpriseID,
                                  EnterpriseName = enpr.EnterpriseName,
                                  ImageLogo = enpr.ImageLogo,
                                  AmountSize = db.EnterpriseSizes.Find(enpr.EnterpriseSize).AmountSize,
                                  NameOfEnterprise = db.TypeOfEnterprises.Find(enpr.TypeOfEnterprise).NameOfEnterprise,
                                  EstablishYear = enpr.EstablishYear,
                                  NameArea = db.Areas.Find(listA.AreaID).NameArea,
                                  listJobId = db.EnterpriseJobs.Where(x => x.EnterpriseID == enpr.EnterpriseID).Select(x => x.JobId).ToList(),
                                  Description = enpr.Description,
                                  UrlAddress = enpr.UrlAddress,
                                  AddressName = enpr.AddressName
                              }).AsEnumerable().Select(x => new ShowFullEnterprise()
                              {
                                  EnterpriseID = x.EnterpriseID,
                                  EnterpriseName = x.EnterpriseName,
                                  ImageLogo = x.ImageLogo,
                                  AmountSize = x.AmountSize,
                                  NameArea = x.NameArea,
                                  NameOfEnterprise = x.NameOfEnterprise,
                                  EstablishYear = x.EstablishYear,
                                  listJobId = x.listJobId,
                                  Description = x.Description,
                                  UrlAddress = x.UrlAddress,
                                  AddressName = x.AddressName
                              });
                var finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                var finalResult2 = new List<ShowFullEnterprise>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].EnterpriseID == finalResult2[j].EnterpriseID)
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
    }
}
