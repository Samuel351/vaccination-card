export interface VaccinationResponse {
  vaccinationId: string;     // Guid → string
  vaccineId: string;         // Guid → string
  vaccineName: string;
  applicationDate: Date;
  doseNumber: number;
}