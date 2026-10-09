public class CEllipse extends CHinhVe {
    protected CDiem tam;
    protected float a;
    protected float b;

    public CEllipse(CDiem tam, float a, float b) {
        this.tam = tam;
        this.a = a;
        this.b = b;
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
        System.out.println("Vẽ Hình Ellipse");
    }
}