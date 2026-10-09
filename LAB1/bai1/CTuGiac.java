public class CTuGiac extends CHinhVe {
    protected CDiem p1;
    protected CDiem p2;
    protected CDiem p3;
    protected CDiem p4;

    public CTuGiac(CDiem p1, CDiem p2, CDiem p3, CDiem p4) {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        this.p4 = p4;
    }

    @Override
    public float chuVi() {
        return p1.khoangCach(p2) + p2.khoangCach(p3) + p3.khoangCach(p4) + p4.khoangCach(p1);
    }

    @Override
    public float dienTich() {
        CTamGiac t1 = new CTamGiac(p1, p2, p3);
        CTamGiac t2 = new CTamGiac(p1, p3, p4);
        return t1.dienTich() + t2.dienTich();
    }

    @Override
    public void ve() {
        System.out.println("Vẽ Hình Tứ Giác");
    }
}