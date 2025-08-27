import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Timepicker } from './timepicker';

describe('Timepicker', () => {
  let component: Timepicker;
  let fixture: ComponentFixture<Timepicker>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Timepicker]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Timepicker);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
