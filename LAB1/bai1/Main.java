import java.util.Scanner;

// Lớp hỗ trợ tọa độ điểm
class CDiem {
    private float x, y;

    public CDiem() {
        this.x = 0;
        this.y = 0;
    }

    public CDiem(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public void nhap(Scanner sc) {
        System.out.print("  Nhập x: ");
        this.x = sc.nextFloat();
        System.out.print("  Nhập y: ");
        this.y = sc.nextFloat();
    }

    public float khoangCach(CDiem d) {
        return (float) Math.sqrt(Math.pow(this.x - d.x, 2) + Math.pow(this.y - d.y, 2));
    }
}

// Lớp cơ sở trừu tượng
abstract class CHinhVe {
    protected int maLoaiHinhVe;

    public abstract void nhap(Scanner sc);
    public abstract float dienTich();
    public abstract float chuVi();
    public abstract void ve();
}

// Lớp Tam Giác
class CTamGiac extends CHinhVe {
    protected CDiem p1 = new CDiem();
    protected CDiem p2 = new CDiem();
    protected CDiem p3 = new CDiem();

    public CTamGiac() {}

    @Override
    public void nhap(Scanner sc) {
        System.out.println("Nhập Điểm P1:");
        p1.nhap(sc);
        System.out.println("Nhập Điểm P2:");
        p2.nhap(sc);
        System.out.println("Nhập Điểm P3:");
        p3.nhap(sc);
    }

    @Override
    public float chuVi() {
        float a = p1.khoangCach(p2);
        float b = p2.khoangCach(p3);
        float c = p3.khoangCach(p1);
        return a + b + c;
    }

    @Override
    public float dienTich() {
        float a = p1.khoangCach(p2);
        float b = p2.khoangCach(p3);
        float c = p3.khoangCach(p1);
        float p = chuVi() / 2;
        return (float) Math.sqrt(p * (p - a) * (p - b) * (p - c));
    }

    @Override
    public void ve() {
        System.out.println("-> Đang vẽ Hình Tam Giác...");
    }
}

// Lớp Tứ Giác
class CTuGiac extends CHinhVe {
    protected CDiem p1 = new CDiem();
    protected CDiem p2 = new CDiem();
    protected CDiem p3 = new CDiem();
    protected CDiem p4 = new CDiem();

    public CTuGiac() {}

    @Override
    public void nhap(Scanner sc) {
        System.out.println("Nhập Điểm P1:");
        p1.nhap(sc);
        System.out.println("Nhập Điểm P2:");
        p2.nhap(sc);
        System.out.println("Nhập Điểm P3:");
        p3.nhap(sc);
        System.out.println("Nhập Điểm P4:");
        p4.nhap(sc);
    }

    @Override
    public float chuVi() {
        return p1.khoangCach(p2) + p2.khoangCach(p3) + p3.khoangCach(p4) + p4.khoangCach(p1);
    }

    @Override
    public float dienTich() {
        // Chia thành 2 tam giác để tính diện tích
        float a1 = p1.khoangCach(p2), b1 = p2.khoangCach(p3), c1 = p3.khoangCach(p1);
        float p1_semi = (a1 + b1 + c1) / 2;
        float dt1 = (float) Math.sqrt(p1_semi * (p1_semi - a1) * (p1_semi - b1) * (p1_semi - c1));

        float a2 = p1.khoangCach(p3), b2 = p3.khoangCach(p4), c2 = p4.khoangCach(p1);
        float p2_semi = (a2 + b2 + c2) / 2;
        float dt2 = (float) Math.sqrt(p2_semi * (p2_semi - a2) * (p2_semi - b2) * (p2_semi - c2));

        return dt1 + dt2;
    }

    @Override
    public void ve() {
        System.out.println("-> Đang vẽ Hình Tứ Giác...");
    }
}

// Lớp Ellipse
class CEllipse extends CHinhVe {
    protected CDiem tam = new CDiem();
    protected float a;
    protected float b;

    public CEllipse() {}

    @Override
    public void nhap(Scanner sc) {
        System.out.println("Nhập Tâm Ellipse:");
        tam.nhap(sc);
        System.out.print("Nhập bán trục a: ");
        this.a = sc.nextFloat();
        System.out.print("Nhập bán trục b: ");
        this.b = sc.nextFloat();
    }

    @Override
    public float dienTich() {
        return (float) (Math.PI * a * b);
    }

    @Override
    public float chuVi() {
        return (float) (Math.PI * (3 * (a + b) - Math.sqrt((3 * a + b) * (a + 3 * b))));
    }

    @Override
    public void ve() {
        System.out.println("-> Đang vẽ Hình Ellipse...");
    }
}

// Lớp Main chạy chương trình
public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        System.out.println("=== NHẬP HÌNH TAM GIÁC ===");
        CHinhVe tamGiac = new CTamGiac();
        tamGiac.nhap(sc);

        System.out.println("\n=== NHẬP HÌNH TỨ GIÁC ===");
        CHinhVe tuGiac = new CTuGiac();
        tuGiac.nhap(sc);

        System.out.println("\n=== NHẬP HÌNH ELLIPSE ===");
        CHinhVe ellipse = new CEllipse();
        ellipse.nhap(sc);

        System.out.println("\n----------------------------------");
        System.out.println("=== KẾT QUẢ TÍNH TOÁN ===");
        
        inThongTin(tamGiac);
        inThongTin(tuGiac);
        inThongTin(ellipse);

        sc.close();
    }

    private static void inThongTin(CHinhVe hinh) {
        hinh.ve();
        System.out.printf(" - Chu vi: %.2f\n", hinh.chuVi());
        System.out.printf(" - Diện tích: %.2f\n", hinh.dienTich());
        System.out.println("----------------------------------");
    }
}