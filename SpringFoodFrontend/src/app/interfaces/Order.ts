import { OrderDetails } from "./OrderDetails";

export interface Order{
    id? : number; 
    name: string;
    email: string; 
    date: Date; 
    orderTotal: number; 
    city: string;
    street: string;
    homeNumber: string;
    apartment: string;
    postalCode : number; 
    orderDetails: OrderDetails[]; 
}