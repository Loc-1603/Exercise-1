using Entities;
using System.Collections.Generic;

namespace BLL
{
    public interface IMonHocService
    {
        List<MonHoc> LayTatCaMonHoc();
        MonHoc LayMonHocTheoMa(string maMH);
        bool ThemMonHoc(MonHoc mh);
        bool XoaMonHoc(string id);
    }
}