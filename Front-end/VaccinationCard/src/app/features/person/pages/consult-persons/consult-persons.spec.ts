import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultPersons } from './consult-persons';

describe('ConsultPersons', () => {
  let component: ConsultPersons;
  let fixture: ComponentFixture<ConsultPersons>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultPersons]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultPersons);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
