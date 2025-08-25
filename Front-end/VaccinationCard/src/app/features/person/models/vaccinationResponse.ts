interface VaccinationResponse {
  vaccineId: string;
  vaccineName: string;
  vaccineDoses: VaccineDose[];
}

interface VaccineDose {
  vaccinationId: string;
  applicationDate: string;
  doseNumber: number;
}