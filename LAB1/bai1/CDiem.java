// Source code is decompiled from a .class file using FernFlower decompiler (from Intellij IDEA).
import java.util.Scanner;

class CDiem {
   private float x;
   private float y;

   public CDiem() {
      this.x = 0.0F;
      this.y = 0.0F;
   }

   public CDiem(float var1, float var2) {
      this.x = var1;
      this.y = var2;
   }

   public void nhap(Scanner var1) {
      System.out.print("  Nhập x: ");
      this.x = var1.nextFloat();
      System.out.print("  Nhập y: ");
      this.y = var1.nextFloat();
   }

   public float khoangCach(CDiem var1) {
      return (float)Math.sqrt(Math.pow((double)(this.x - var1.x), (double)2.0F) + Math.pow((double)(this.y - var1.y), (double)2.0F));
   }
}
