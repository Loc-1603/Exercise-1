using System;
using BLL;
using Entities;

class Program
{
    static SinhVienService svService = new SinhVienService();
    static MonHocService mhService = new MonHocService();
    static DangKyService dkService = new DangKyService();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║   HỆ THỐNG QUẢN LÝ SINH VIÊN v1.0   ║");
        Console.WriteLine("║     Layered Architecture Demo        ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n======== MENU CHÍNH ========");
            Console.WriteLine("1. Quản lý Sinh Viên");
            Console.WriteLine("2. Quản lý Môn Học");
            Console.WriteLine("3. Đăng Ký & Điểm");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": MenuSinhVien(); break;
                case "2": MenuMonHoc(); break;
                case "3": MenuDangKy(); break;
                case "0": running = false; break;
                default: Console.WriteLine("❌ Lựa chọn không hợp lệ!"); break;
            }
        }

        Console.WriteLine("\n👋 Tạm biệt!");
    }

    // ==================== MENU SINH VIÊN ====================
    static void MenuSinhVien()
    {
        Console.WriteLine("\n--- QUẢN LÝ SINH VIÊN ---");
        Console.WriteLine("1. Xem danh sách sinh viên");
        Console.WriteLine("2. Thêm sinh viên mới");
        Console.WriteLine("3. Xóa sinh viên");
        Console.WriteLine("4. Tìm kiếm sinh viên");
        Console.Write("Chọn: ");

        string ch = Console.ReadLine() ?? "";
        switch (ch)
        {
            case "1": HienThiDanhSachSV(); break;
            case "2": ThemSinhVien(); break;
            case "3": XoaSinhVien(); break;
            case "4": TimKiemSinhVien(); break;
        }
    }

    static void HienThiDanhSachSV()
    {
        var list = svService.LayTatCaSinhVien();
        Console.WriteLine($"\n📋 Danh sách sinh viên ({list.Count} người):");
        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"{"Mã SV",-10} {"Họ Tên",-25} {"Email",-25} {"Ngày Sinh",-12}");
        Console.WriteLine(new string('-', 70));

        foreach (var sv in list)
        {
            Console.WriteLine($"{sv.MaSV,-10} {sv.HoTen,-25} {sv.Email,-25} {sv.NgaySinh:dd/MM/yyyy}");
        }
    }

    static void ThemSinhVien()
    {
        Console.WriteLine("\n➕ THÊM SINH VIÊN MỚI");
        Console.Write("Mã SV: "); string maSV = Console.ReadLine() ?? "";
        Console.Write("Họ tên: "); string hoTen = Console.ReadLine() ?? "";
        Console.Write("Email: "); string email = Console.ReadLine() ?? "";
        Console.Write("Ngày sinh (dd/MM/yyyy): "); string ngaySinhStr = Console.ReadLine() ?? "";

        DateTime ngaySinh;
        if (!DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", null,
            System.Globalization.DateTimeStyles.None, out ngaySinh))
        {
            Console.WriteLine("❌ Định dạng ngày không hợp lệ!");
            return;
        }

        var sv = new SinhVien
        {
            MaSV = maSV,
            HoTen = hoTen,
            Email = email,
            NgaySinh = ngaySinh
        };

        bool result = svService.ThemSinhVien(sv);
        if (result) Console.WriteLine("✅ Thêm sinh viên thành công!");
    }

    static void XoaSinhVien()
    {
        Console.Write("\nNhập Mã SV cần xóa: ");
        string maSV = Console.ReadLine() ?? "";
        var sv = svService.LaySinhVienTheoMa(maSV);
        if (sv == null) { Console.WriteLine("❌ Không tìm thấy sinh viên!"); return; }

        Console.Write($"Xác nhận xóa sinh viên '{sv.HoTen}'? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            bool result = svService.XoaSinhVien(sv.Id);
            if (result) Console.WriteLine("✅ Xóa thành công!");
        }
    }

    static void TimKiemSinhVien()
    {
        Console.Write("\nNhập Mã SV cần tìm: ");
        string maSV = Console.ReadLine() ?? "";
        var sv = svService.LaySinhVienTheoMa(maSV);

        if (sv == null) { Console.WriteLine("❌ Không tìm thấy!"); return; }

        Console.WriteLine($"\n📌 Thông tin sinh viên:");
        Console.WriteLine($"   Mã SV   : {sv.MaSV}");
        Console.WriteLine($"   Họ tên  : {sv.HoTen}");
        Console.WriteLine($"   Email   : {sv.Email}");
        Console.WriteLine($"   Ngày sinh: {sv.NgaySinh:dd/MM/yyyy}");
    }

    // ==================== MENU MÔN HỌC ====================
    static void MenuMonHoc()
    {
        Console.WriteLine("\n--- QUẢN LÝ MÔN HỌC ---");
        Console.WriteLine("1. Xem danh sách môn học");
        Console.WriteLine("2. Thêm môn học mới");
        Console.Write("Chọn: ");

        string ch = Console.ReadLine() ?? "";
        switch (ch)
        {
            case "1": HienThiMonHoc(); break;
            case "2": ThemMonHoc(); break;
        }
    }

    static void HienThiMonHoc()
    {
        var list = mhService.LayTatCaMonHoc();
        Console.WriteLine($"\n📚 Danh sách môn học ({list.Count} môn):");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"{"Mã MH",-10} {"Tên Môn Học",-30} {"Tín Chỉ",-10}");
        Console.WriteLine(new string('-', 50));

        foreach (var mh in list)
        {
            Console.WriteLine($"{mh.MaMH,-10} {mh.TenMH,-30} {mh.SoTinChi,-10}");
        }
    }

    static void ThemMonHoc()
    {
        Console.WriteLine("\n➕ THÊM MÔN HỌC MỚI");
        Console.Write("Mã môn học: "); string maMH = Console.ReadLine() ?? "";
        Console.Write("Tên môn học: "); string tenMH = Console.ReadLine() ?? "";
        Console.Write("Số tín chỉ: "); int soTinChi = int.Parse(Console.ReadLine() ?? "0");

        var mh = new MonHoc { MaMH = maMH, TenMH = tenMH, SoTinChi = soTinChi };
        bool result = mhService.ThemMonHoc(mh);
        if (result) Console.WriteLine("✅ Thêm môn học thành công!");
    }

    // ==================== MENU ĐĂNG KÝ ====================
    static void MenuDangKy()
    {
        Console.WriteLine("\n--- ĐĂNG KÝ & ĐIỂM ---");
        Console.WriteLine("1. Đăng ký môn học");
        Console.WriteLine("2. Xem đăng ký của sinh viên");
        Console.WriteLine("3. Nhập điểm");
        Console.WriteLine("4. Xem điểm trung bình");
        Console.Write("Chọn: ");

        string ch = Console.ReadLine() ?? "";
        switch (ch)
        {
            case "1": DangKyMonHoc(); break;
            case "2": XemDangKy(); break;
            case "3": NhapDiem(); break;
            case "4": XemDiemTrungBinh(); break;
        }
    }

    static void DangKyMonHoc()
    {
        Console.Write("\nMã sinh viên: "); string maSV = Console.ReadLine() ?? "";
        Console.Write("Mã môn học: "); string maMH = Console.ReadLine() ?? "";

        bool result = dkService.DangKyMonHoc(maSV, maMH);
        if (result) Console.WriteLine("✅ Đăng ký thành công!");
    }

    static void XemDangKy()
    {
        Console.Write("\nNhập Mã SV: "); string maSV = Console.ReadLine() ?? "";
        var list = dkService.LayDangKyTheоSinhVien(maSV);

        Console.WriteLine($"\n📋 Danh sách đăng ký của SV {maSV}:");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"{"Mã MH",-10} {"Ngày ĐK",-15} {"Điểm",-10}");
        Console.WriteLine(new string('-', 50));

        foreach (var dk in list)
        {
            string diem = dk.Diem.HasValue ? dk.Diem.Value.ToString("F1") : "Chưa có";
            Console.WriteLine($"{dk.MaMH,-10} {dk.NgayDangKy:dd/MM/yyyy,-15} {diem,-10}");
        }
    }

    static void NhapDiem()
    {
        Console.Write("\nMã sinh viên: "); string maSV = Console.ReadLine() ?? "";
        Console.Write("Mã môn học: "); string maMH = Console.ReadLine() ?? "";
        Console.Write("Điểm (0-10): "); double diem = double.Parse(Console.ReadLine() ?? "0");

        bool result = dkService.NhapDiem(maSV, maMH, diem);
        if (result) Console.WriteLine("✅ Nhập điểm thành công!");
    }

    static void XemDiemTrungBinh()
    {
        Console.Write("\nNhập Mã SV: "); string maSV = Console.ReadLine() ?? "";
        double dtb = dkService.TinhDiemTrungBinh(maSV);
        Console.WriteLine($"\n📊 Điểm trung bình của SV {maSV}: {dtb:F2}");

        string xepLoai = dtb >= 8.5 ? "Giỏi" :
                         dtb >= 7.0 ? "Khá" :
                         dtb >= 5.5 ? "Trung bình" : "Yếu";
        Console.WriteLine($"   Xếp loại: {xepLoai}");
    }
}
﻿
