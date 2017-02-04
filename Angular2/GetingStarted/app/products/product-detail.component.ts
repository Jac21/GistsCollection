import { Component } from '@angular/core';

import { IProduct } from './product';

@Component({
    selector: 'product-detail',
    moduleId: module.id,
    templateUrl: 'product-detail.component.html'
})

export class ProductDetailComponent {
    pageTitle: string = 'Product Detail';
    product: IProduct;
}
