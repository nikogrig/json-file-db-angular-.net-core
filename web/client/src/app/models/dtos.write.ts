export class User {
    id: number;
    username: string;
    email: string;
    firstname: string;
    lastname: string;
    telephone: string;
    password: string;
    confirmPassword: string;
}

export class Role {
    id: number;
    role: string;
}

export class RegisterModel {
    constructor(
      public username: string,
      public password: string,
      public confirmPassword: string,
      public email: string,
      public telephone: string,
      public firstname: string,
      public lastname: string) {}
}

export class LoginModel {
    constructor( 
      public email: string,
      public password: string){      
      }
   }