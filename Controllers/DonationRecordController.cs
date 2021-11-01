using Dapper;
using MdyBloolDonation.Abstract;
using MdyBloolDonation.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdyBloolDonation.Controllers
{
    class DonationRecordController:DonationDB
    {
        #region "Getting the list of donor who donote blood"
        public List<BloodDonor> getBloodDonorList(string D_ID,string D_Name, out string error)
        {
            List<BloodDonor> itemlist = new List<BloodDonor>();
            error = "";
            string sql = "exec get_blood_donor_list @d_id,@d_name";
            var parameters = new DynamicParameters();
            parameters.Add("d_id", D_ID, DbType.String);
            parameters.Add("d_name", D_Name, DbType.String);
            try
            {
                itemlist = DNDB().Query<BloodDonor>(sql, param: parameters).ToList();
                error = "";
            }
            catch (Exception e)
            {
                error = "In exception case";
            }
            return itemlist;
        }
        #endregion
        #region "Getting the list of bloodtype"
        public List<BloodType> getBloodTypeList(string bloodtype, out string error)
        {
            List<BloodType> bloodlist = new List<BloodType>();
            error = "";
            string sql = "exec get_bloodtype @p_bloodtype";
            var parameters = new DynamicParameters();
            parameters.Add("p_bloodtype", bloodtype, DbType.String);
            try
            {
                bloodlist = DNDB().Query<BloodType>(sql, param: parameters).ToList();
                error = "";
            }
            catch (Exception e)
            {
                error = "In exception case";
            }
            return bloodlist;
        }
        #endregion
        #region "Getting the list of gender"
        public List<Gender> getGenderList(string gender, out string error)
        {
            List<Gender> genderlist = new List<Gender>();
            error = "";
            string sql = "exec get_gender @p_gender";
            var parameters = new DynamicParameters();
            parameters.Add("p_gender", gender, DbType.String);
            try
            {
                genderlist = DNDB().Query<Gender>(sql, param: parameters).ToList();
                error = "";
            }
            catch (Exception e)
            {
                error = "In exception case";
            }
            return genderlist;
        }
        #endregion
        #region "Getting the list of gender"
        public bool saveBloodDonor(BloodDonor objdonor, out string error)
        {
            bool result = true;
            error = "";
            string sql = "exec save_blooddonor @pd_id,@pd_name,@pstartdate,@plastdate,@paddress," +
                "@pphoneno,@pemail,@ptimes,@pbloodtype,@pgender,@pisactive,@page,@pbirthdate";
            var parameters = new DynamicParameters();
            parameters.Add("pd_id",objdonor.d_id, DbType.String);
            parameters.Add("pd_name", objdonor.d_name, DbType.String);
            parameters.Add("pstartdate", objdonor.startdate, DbType.DateTime);
            parameters.Add("plastdate", objdonor.lastdate, DbType.DateTime);
            parameters.Add("paddress", objdonor.address, DbType.String);
            parameters.Add("pphoneno", objdonor.phoneno, DbType.String);
            parameters.Add("pemail", objdonor.email, DbType.String);
            parameters.Add("ptimes", objdonor.times, DbType.Int32);
            parameters.Add("pbloodtype", objdonor.bloodtype, DbType.String);
            parameters.Add("pgender", objdonor.gender, DbType.String);
            parameters.Add("pisactive",1, DbType.Boolean);
            parameters.Add("page", objdonor.age, DbType.Int16);
            parameters.Add("pbirthdate", objdonor.birthdate, DbType.DateTime);
            try
            {
                int ind = DNDB().Execute(sql,param:parameters);
                error = "";
                result = true;
            }
            catch (Exception e)
            {
                error = "In exception case";
                result = false;
            }
            return result;
        }
        #endregion
        #region "Getting Donor Person ID"
        public string getID(out string error)
        {
            string result = "";
            error = "";
            string sql = "exec get_DonorID";            
            try
            {
                result = Convert.ToString(DNDB().ExecuteScalar(sql));
                error = "";
            }
            catch (Exception e)
            {
                error = "In exception case";
                result = "";
            }
            return result;
        }
        #endregion
        #region "Updating Donor"
        public bool deleteDonor(string p_did,out string error)
        {
            bool result = true;
            error = "";
            string sql = "exec delete_donor @p_did";
            var parameters = new DynamicParameters();
            parameters.Add("p_did", p_did, DbType.String);
            try
            {
                int red = DNDB().Execute(sql, param: parameters);
                result = (red > 0) ? true : false;
            }
            catch (Exception e)
            {
                error = "In exception case";
                result = false;
            }
            return result;
        }
        #endregion
        #region "Saving Donation Transaction" 
        public bool saveDonationTransaction(string p_d_id,
            DateTime p_d_date,
            string p_remarks,
            out string error)
        {
            bool result = true;
            error = "";
            string sql = "exec save_donationtransaction @p_d_id,@p_d_date,@p_remarks,@p_isactive";
            var parameters = new DynamicParameters();
            parameters.Add("p_d_id", p_d_id, DbType.String);
            parameters.Add("p_d_date", p_d_date, DbType.DateTime);
            parameters.Add("p_remarks", p_remarks, DbType.String);
            parameters.Add("p_isactive", 1, DbType.Boolean);

            try
            {
                int red = DNDB().Execute(sql, param: parameters);
                result = true;
            }
            catch (Exception e)
            {
                error = "In exception case";
                result = false;
            }
            return result;
        }
        #endregion
        #region "Get the donated list"
        public List<DonorReport1> getAvailableDonorList(string p_did,
            DateTime p_date,
            string p_gender,
            string p_bloodtype,out string error
            )
        {
            List<DonorReport1> rep = new List<DonorReport1>();
            error = "";
            string sql = "exec get_blooddonated_info @p_did,@p_date,@p_gender,@p_bloodtype";
            var parameters = new DynamicParameters();
            parameters.Add("p_did", p_did, DbType.String);
            parameters.Add("p_date", p_date, DbType.DateTime);
            parameters.Add("p_gender", p_gender, DbType.String);
            parameters.Add("p_bloodtype", p_bloodtype, DbType.String);
            try
            {
                rep = DNDB().Query<DonorReport1>(sql, param: parameters).ToList();
            }catch(Exception e)
            {
                error = e.Message;
            }
            return rep;
        }
        #endregion



        #region "StaffInformation"

        #region "GetStaf"
        public List<Staff> getStaffList(string p_name,out string error)
        {
            List<Staff> rep = new List<Staff>();
            error = "";
            string sql = "exec get_staff @p_name";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_name, DbType.String);
            try
            {
                rep = DNDB().Query<Staff>(sql, param: parameters).ToList();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return rep;
        }
        #endregion
        #region "Get Login Staff"
        public List<Staff> getLoginStaffList(string p_name,string p_password, out string error)
        {
            List<Staff> rep = new List<Staff>();
            error = "";
            string sql = "exec get_login_staff @p_name,@p_password";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_name, DbType.String);
            parameters.Add("p_password", p_password, DbType.String);
            try
            {
                rep = DNDB().Query<Staff>(sql, param: parameters).ToList();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return rep;
        }
        #endregion
        #region "SaveStaff"
        public bool saveStaff(Staff p_staff, out string error)
        {            
            error = "";
            bool ret = true;
            string sql = "exec save_staff @p_name,@p_role,@p_email,@p_password,@p_phoneno,@p_address,@p_isactive,@p_createddate";
            var parameters = new DynamicParameters();
            parameters.Add("p_name",p_staff.name, DbType.String);
            parameters.Add("p_role", p_staff.role, DbType.String);
            parameters.Add("p_email", p_staff.email, DbType.String);
            parameters.Add("p_password", p_staff.password, DbType.String);
            parameters.Add("p_phoneno", p_staff.phoneno, DbType.String);
            parameters.Add("p_address", p_staff.address, DbType.String);
            parameters.Add("p_isactive", p_staff.isactive, DbType.Boolean);
            parameters.Add("p_createddate", p_staff.createddate, DbType.DateTime);
            try
            {
                int re = DNDB().Execute(sql, param: parameters);
                ret = true;
            }
            catch (Exception e)
            {
                error = e.Message;
                ret = false;
            }
            return ret;
        }
        #endregion

        #region "Delete Staff"
        public bool deleteStaff(string p_name, out string error)
        {
            error = "";
            bool ret = true;
            string sql = "exec delete_staff @p_name";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_name, DbType.String);
            try
            {
                int re = DNDB().Execute(sql, param: parameters);
                ret = true;
            }
            catch (Exception e)
            {
                error = e.Message;
                ret = false;
            }
            return ret;
        }
        #endregion
        #region "GetRole"
        public List<Role> getRoleList(string p_name, out string error)
        {
            List<Role> rep = new List<Role>();
            error = "";
            string sql = "exec get_role @p_name";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_name, DbType.String);
            try
            {
                rep = DNDB().Query<Role>(sql, param: parameters).ToList();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return rep;
        }
        #endregion
        #region "Save Role"
        public bool saveRole(Role p_role, out string error)
        {
            error = "";
            bool ret = true;
            string sql = "exec save_role @p_name,@p_active";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_role.name, DbType.String);
            parameters.Add("p_active", p_role.isactive, DbType.Boolean);            
            try
            {
                int re = DNDB().Execute(sql, param: parameters);
                ret = true;
            }
            catch (Exception e)
            {
                error = e.Message;
                ret = false;
            }
            return ret;
        }
        #endregion
        #region "delete Role"
        public bool deleteRole(string p_role, out string error)
        {
            error = "";
            bool ret = true;
            string sql = "exec delete_role @p_name";
            var parameters = new DynamicParameters();
            parameters.Add("p_name", p_role, DbType.String);
            try
            {
                int re = DNDB().Execute(sql, param: parameters);
                ret = true;
            }
            catch (Exception e)
            {
                error = e.Message;
                ret = false;
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
