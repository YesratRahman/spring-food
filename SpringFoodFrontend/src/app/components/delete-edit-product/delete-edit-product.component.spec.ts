import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteEditProductComponent } from './delete-edit-product.component';

describe('DeleteEditProductComponent', () => {
  let component: DeleteEditProductComponent;
  let fixture: ComponentFixture<DeleteEditProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteEditProductComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteEditProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
