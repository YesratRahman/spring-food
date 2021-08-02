import { CartProduct } from "./CartProduct";

export class Cart{
    items:CartProduct[] = [];

    get totalPrice(): number{
        let totalPrice = 0;
        this.items.forEach(item => {
            totalPrice += item.price;
        });

        return totalPrice;
    }
    get tax(): number{
        let tax = 0; 
        this.items.forEach(item => {
            tax = item.price * 0.025;
        });
        return tax; 
    }
}