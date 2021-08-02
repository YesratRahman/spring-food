import { Product } from "./Product";

export interface Category{
    name: string,
    id? : number, 
    products: Product[]
}