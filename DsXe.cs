using System;
using System.Collections.Generic;
using System.Linq;

namespace ChuongTrinhQuanLyXe
{
    class DsXe
    {
        private readonly List<Xe> DanhSach = new();
        private readonly HashSet<string> TapBienSoDaCo = new(); // đảm bảo biển số duy nhất

        // ===== 1. Thêm xe ô tô =====
        private void ThemXeOto()
        {
            var xe = new XeOto();
            xe.nhapThongTinXe(TapBienSoDaCo);
            DanhSach.Add(xe);
            Console.WriteLine("Đã thêm Ô TÔ.\n");
        }

        // ===== 2. Thêm xe tải =====
        private void ThemXeTai()
        {
            var xe = new XeTai();
            xe.nhapThongTinXe(TapBienSoDaCo);
            DanhSach.Add(xe);
            Console.WriteLine("Đã thêm XE TẢI.\n");
        }

        // ===== 3. Xuất danh sách tất cả =====
        private void XuatTatCa()
        {
            Console.WriteLine("=== DANH SÁCH TẤT CẢ XE ===");
            if (DanhSach.Count == 0)
            {
                Console.WriteLine("(Danh sách rỗng)\n");
                return;
            }

            var oto = DanhSach.OfType<XeOto>().ToList();
            var tai = DanhSach.OfType<XeTai>().ToList();

            Console.WriteLine("\n-- Ô TÔ --");
            if (oto.Count == 0) Console.WriteLine("(Không có)");
            else oto.ForEach(x => x.xuatThongTinXe());

            Console.WriteLine("\n-- XE TẢI --");
            if (tai.Count == 0) Console.WriteLine("(Không có)");
            else tai.ForEach(x => x.xuatThongTinXe());

            Console.WriteLine();
        }

        // ===== 4. Tìm ô tô nhiều chỗ nhất =====
        private List<XeOto> TimOtoSoChoMax()
        {
            var oto = DanhSach.OfType<XeOto>().ToList();
            if (oto.Count == 0) return new List<XeOto>();
            int maxCho = oto.Max(x => x.SoChoNgoi);
            return oto.Where(x => x.SoChoNgoi == maxCho).ToList();
        }

        private void XuatOtoSoChoMax()
        {
            var dsMax = TimOtoSoChoMax();
            if (dsMax.Count == 0)
            {
                Console.WriteLine("Không có xe ô tô nào để so sánh!\n");
                return;
            }

            Console.WriteLine($"Có {dsMax.Count} ô tô có số chỗ lớn nhất:");
            int i = 1;
            foreach (var xe in dsMax)
            {
                Console.WriteLine($"\nÔ tô {i++}:");
                xe.xuatThongTinXe();
            }
            Console.WriteLine();
        }

        // ===== 5. Sắp xếp xe tải theo trọng tải tăng dần =====
        private void SapXepXeTaiTheoTrongTai()
        {
            var tai = DanhSach.OfType<XeTai>().OrderBy(x => x.TrongTaiTan).ToList();
            if (tai.Count == 0)
            {
                Console.WriteLine("Không có xe tải để sắp xếp.\n");
                return;
            }

            Console.WriteLine("=== XE TẢI (tăng dần theo trọng tải) ===");
            tai.ForEach(x => x.xuatThongTinXe());
            Console.WriteLine();
        }

        // ===== 6. Xuất danh sách biển số đẹp =====
        private void XuatBienSoDep()
        {
            var dep = DanhSach.Where(x => x.LaBienSoDep()).ToList();
            if (dep.Count == 0)
            {
                Console.WriteLine("Không có biển số đẹp nào.\n");
                return;
            }

            Console.WriteLine("=== DANH SÁCH XE CÓ BIỂN SỐ ĐẸP ===");
            dep.ForEach(x => x.xuatThongTinXe());
            Console.WriteLine();
        }

        // ===== 7. Xe thuộc TP.HCM =====
        private void XuatXeThuocTPHCM()
        {
            var otoHCM = DanhSach.OfType<XeOto>().Where(x => x.ThuocTPHCM()).ToList();
            var taiHCM = DanhSach.OfType<XeTai>().Where(x => x.ThuocTPHCM()).ToList();

            Console.WriteLine("=== XE THUỘC TP.HCM (giản lược: 41 hoặc 50..59) ===");

            Console.WriteLine("\n-- Ô TÔ HCM --");
            if (otoHCM.Count == 0) Console.WriteLine("(Không có)");
            else otoHCM.ForEach(x => x.xuatThongTinXe());

            Console.WriteLine("\n-- XE TẢI HCM --");
            if (taiHCM.Count == 0) Console.WriteLine("(Không có)");
            else taiHCM.ForEach(x => x.xuatThongTinXe());

            Console.WriteLine();
        }

 
        private void XuatDangKiemSapToi()
        {
            if (DanhSach.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng!\n");
                return;
            }

            Console.WriteLine("=== ĐĂNG KIỂM SẮP TỚI CHO TỪNG XE ===");
            foreach (var xe in DanhSach)
            {
                if (xe is XeOto o)
                {
                    var (han, phi) = o.TinhDangKiemVaPhi();
                    Console.WriteLine("\n[Ô TÔ]");
                    o.xuatThongTinXe();
                    Console.WriteLine($"- Hạn đăng kiểm tiếp theo: {han:dd/MM/yyyy}");
                    Console.WriteLine($"- Phí dự kiến: {phi:N0} ₫");
                }
                else if (xe is XeTai t)
                {
                    var (han, phi) = t.TinhDangKiemVaPhi();
                    Console.WriteLine("\n[XE TẢI]");
                    t.xuatThongTinXe();
                    Console.WriteLine($"- Hạn đăng kiểm tiếp theo: {han:dd/MM/yyyy}");
                    Console.WriteLine($"- Phí dự kiến: {phi:N0} ₫");
                }
            }
            Console.WriteLine();
        }
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("ASM2.3 - CHƯƠNG TRÌNH QUẢN LÝ XE");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("1. Thêm xe ô tô");
                Console.WriteLine("2. Thêm xe tải");
                Console.WriteLine("3. Xuất danh sách tất cả xe");
                Console.WriteLine("4. Tìm ô tô có số chỗ ngồi nhiều nhất");
                Console.WriteLine("5. Sắp xếp xe tải theo trọng tải tăng dần");
                Console.WriteLine("6. Xuất danh sách các biển số đẹp");
                Console.WriteLine("7. Danh sách Ô Tô & Xe Tải thuộc TP.HCM");
                Console.WriteLine("8. Thời gian & phí đăng kiểm sắp tới");
                Console.Write("Chọn chức năng: ");

                if (!int.TryParse(Console.ReadLine(), out int chon))
                {
                    Console.WriteLine("Vui lòng nhập số hợp lệ!\n");
                    continue;
                }

                Console.WriteLine();
                switch (chon)
                {
                    case 0: return;
                    case 1: ThemXeOto(); break;
                    case 2: ThemXeTai(); break;
                    case 3: XuatTatCa(); break;
                    case 4: XuatOtoSoChoMax(); break;
                    case 5: SapXepXeTaiTheoTrongTai(); break;
                    case 6: XuatBienSoDep(); break;
                    case 7: XuatXeThuocTPHCM(); break;
                    case 8: XuatDangKiemSapToi(); break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!\n");
                        break;
                }
            }
        }
    }
}
