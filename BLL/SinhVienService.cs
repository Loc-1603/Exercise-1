using DAL;
using Entities;
using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class SinhVienService : ISinhVienService
    {
        private readonly SinhVienRepository _repo;

        public SinhVienService()
        {
            _repo = new SinhVienRepository();
        }

        public List<SinhVien> LayTatCaSinhVien()
        {
            return _repo.GetAll();
        }

        public SinhVien LaySinhVienTheoMa(string maSV)
        {
            return _repo.GetByMaSV(maSV);
        }

        public bool ThemSinhVien(SinhVien sv)
        {
            // Kiểm tra logic: không được trùng mã SV
            var existing = _repo.GetByMaSV(sv.MaSV);
            if (existing != null)
            {
                Console.WriteLine("❌ Mã sinh viên đã tồn tại!");
                return false;
            }

            // Kiểm tra email hợp lệ
            if (!sv.Email.Contains("@"))
            {
                Console.WriteLine("❌ Email không hợp lệ!");
                return false;
            }

            _repo.Insert(sv);
            return true;
        }

        public bool CapNhatSinhVien(string id, SinhVien sv)
        {
            var existing = _repo.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên!");
                return false;
            }
            _repo.Update(id, sv);
            return true;
        }

        public bool XoaSinhVien(string id)
        {
            var existing = _repo.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên!");
                return false;
            }
            _repo.Delete(id);
            return true;
        }
    }
}