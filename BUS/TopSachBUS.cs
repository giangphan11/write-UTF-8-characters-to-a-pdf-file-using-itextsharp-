using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BUS
{
    public class TopSachBUS
    {
        TopSachAccess sachAccess = new TopSachAccess();
        public List<TopSach> dsTopSach()
        {
            return sachAccess.layDanhSachTop10Sach();
        }
        public List<TopSach> dsTopSach(DateTime start, DateTime end)
        {
            return sachAccess.layDanhSachTop10Sach(start,end);
        }
    }
}
