using DAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class MonHocService : IMonHocService
    {
        private readonly MonHocRepository _repo;

        public MonHocService()
        {
            _repo = new MonHocRepository();
        }

        public List<MonHoc> LayTatCaMonHoc()
        {
            return _repo.GetAll();
        }

        public MonHoc LayMonHocTheoMa(string maMH)
        {
            return _repo.GetByMaMH(maMH);
        }

        public bool ThemMonHoc(MonHoc mh)
        {
            // Kiểm tra trùng mã môn học
            var existing = _repo.GetByMaMH(mh.MaMH);
            if (existing != null)
            {
                Console.WriteLine("❌ Mã môn học đã tồn tại!");
                return false;
            }

            // Số tín chỉ phải từ 1-10
            if (mh.SoTinChi < 1 || mh.SoTinChi > 10)
            {
                Console.WriteLine("❌ Số tín chỉ phải từ 1 đến 10!");
                return false;
            }

            _repo.Insert(mh);
            return true;
        }

        public bool XoaMonHoc(string id)
        {
            var existing = _repo.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("❌ Không tìm thấy môn học!");
                return false;
            }
            _repo.Delete(id);
            return true;
        }
    }
}