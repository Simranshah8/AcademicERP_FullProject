using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Wallet
{
    public class Khalti
    {
        DA.Wallet.KhaltiDB db = null;
        int _UserId = 0;
        public Khalti(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.Wallet.KhaltiDB(hostName, dbName);
        }
        public ResponeValues SaveWalletLog(BE.Wallet.WalletRequest beData)
        {
            return db.SaveWalletLog(beData);
        }
        public ResponeValues SaveWalletConfirmationLog(BE.Wallet.WalletRequest beData)
        {
            return db.SaveWalletConfirmationLog(beData);
        }
        public ResponeValues SaveWalletVerificationLog(BE.Wallet.WalletVerificationLog beData)
        {
            return db.SaveWalletVerificationLog(beData);
        }
        public AcademicLib.BE.Wallet.WalletRequest getWalletToken(int? EmployeeId,int? StudentId, string NewGuiId)
        {
            return db.getWalletToken(_UserId,EmployeeId,StudentId, NewGuiId);
        }
    }
}
