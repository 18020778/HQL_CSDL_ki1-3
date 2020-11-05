using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class WorkInvitationDao
    {
        CareerWeb db = null;
        public WorkInvitationDao()
        {
            db = new CareerWeb();
        }

        public WorkInvitation findWorkInvitation(String userId, Guid offerId)
        {
            return db.WorkInvitations.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);

        }

        public List<WorkInvitation> ListInvitationByUser(String userId)
        {
            var workinvitationList = db.WorkInvitations.ToList();
            var result = from workinvitation in workinvitationList
                         where workinvitation.UserID == userId
                         select workinvitation;

            return result.ToList();
        }

        public bool InsertWorkInvitation(WorkInvitation workInvitation)
        {
            try
            {
                db.WorkInvitations.Add(workInvitation);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStatus(String userId, Guid offerId, String status)
        {
            try
            {
                var workinvitation = findWorkInvitation(userId, offerId);
                workinvitation.Status = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
