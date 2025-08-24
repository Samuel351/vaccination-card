import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultVaccines } from './consult-vaccines';

describe('ConsultVaccines', () => {
  let component: ConsultVaccines;
  let fixture: ComponentFixture<ConsultVaccines>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultVaccines]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultVaccines);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
