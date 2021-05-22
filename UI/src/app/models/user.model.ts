export class User {
  id: string;
  email: string;
  firstName: string;
  middleName: string;
  lastName: string;
  photo?: string;
  phone: string;
  password?: string;
  jwtToken?: string;
  active: boolean;
  role:string;
}
