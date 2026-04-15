using Entities;
using System.Collections.Generic;

namespace BLL
{
    public interface IDangKyService
    {
        List<DangKy> LayTatCaDangKy();
        List<DangKy> LayDangKyTheоSinhVien(string maSV);
        bool DangKyMonHoc(string maSV, string maMH);
        bool NhapDiem(string maSV, string maMH, double diem);
        double TinhDiemTrungBinh(string maSV);
    }
}
﻿
