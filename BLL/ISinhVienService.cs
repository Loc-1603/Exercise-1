using Entities;
using System.Collections.Generic;

namespace BLL
{
    public interface ISinhVienService
    {
        List<SinhVien> LayTatCaSinhVien();
        SinhVien LaySinhVienTheoMa(string maSV);
        bool ThemSinhVien(SinhVien sv);
        bool CapNhatSinhVien(string id, SinhVien sv);
        bool XoaSinhVien(string id);
    }
}
﻿
