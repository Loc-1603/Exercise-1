using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class DangKyService : IDangKyService
    {
        private readonly DangKyRepository _dkRepo;
        private readonly SinhVienRepository _svRepo;
        private readonly MonHocRepository _mhRepo;

        public DangKyService()
        {
            _dkRepo = new DangKyRepository();
            _svRepo = new SinhVienRepository();
            _mhRepo = new MonHocRepository();
        }

        public List<DangKy> LayTatCaDangKy()
        {
            return _dkRepo.GetAll();
        }

        public List<DangKy> LayDangKyTheоSinhVien(string maSV)
        {
            return _dkRepo.GetBySinhVien(maSV);
        }

        public bool DangKyMonHoc(string maSV, string maMH)
        {
            // Kiểm tra sinh viên tồn tại không
            var sv = _svRepo.GetByMaSV(maSV);
            if (sv == null)
            {
                Console.WriteLine("❌ Sinh viên không tồn tại!");
                return false;
            }

            // Kiểm tra môn học tồn tại không
            var mh = _mhRepo.GetByMaMH(maMH);
            if (mh == null)
            {
                Console.WriteLine("❌ Môn học không tồn tại!");
                return false;
            }

            // Kiểm tra đã đăng ký chưa
            var dsDangKy = _dkRepo.GetBySinhVien(maSV);
            bool daDangKy = dsDangKy.Any(dk => dk.MaMH == maMH);
            if (daDangKy)
            {
                Console.WriteLine("❌ Sinh viên đã đăng ký môn học này rồi!");
                return false;
            }

            var dangKy = new DangKy
            {
                MaSV = maSV,
                MaMH = maMH,
                NgayDangKy = DateTime.Now,
                Diem = null
            };

            _dkRepo.Insert(dangKy);
            return true;
        }

        public bool NhapDiem(string maSV, string maMH, double diem)
        {
            // Kiểm tra điểm hợp lệ
            if (diem < 0 || diem > 10)
            {
                Console.WriteLine("❌ Điểm phải từ 0 đến 10!");
                return false;
            }

            var dsDangKy = _dkRepo.GetBySinhVien(maSV);
            var dk = dsDangKy.FirstOrDefault(d => d.MaMH == maMH);

            if (dk == null)
            {
                Console.WriteLine("❌ Không tìm thấy đăng ký này!");
                return false;
            }

            dk.Diem = diem;
            _dkRepo.Update(dk.Id, dk);
            return true;
        }

        public double TinhDiemTrungBinh(string maSV)
        {
            var dsDangKy = _dkRepo.GetBySinhVien(maSV);
            var dsCoDiem = dsDangKy.Where(dk => dk.Diem.HasValue).ToList();

            if (!dsCoDiem.Any()) return 0;

            return dsCoDiem.Average(dk => dk.Diem.Value);
        }
    }
}