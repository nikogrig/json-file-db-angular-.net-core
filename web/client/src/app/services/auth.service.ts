import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as DtoWrite from '../models/dtos.write';

const baseUrl = "https://localhost:5001/api/account"

@Injectable()
export class AuthService {
     
    constructor(private http: HttpClient) {}
 
    register(registerModel: DtoWrite.RegisterModel) : Observable<DtoWrite.RegisterModel> {
        return this.http.post<DtoWrite.RegisterModel>(`${baseUrl}/register`,  registerModel);
    }

    login(loginModel: DtoWrite.LoginModel) : Observable<DtoWrite.LoginModel> {
        return this.http.post<DtoWrite.LoginModel>(`${baseUrl}/login`, loginModel)   
    }

    logout() : Observable<Object> {
        return this.http.post(`${baseUrl}/logout`, '');
    }

     authenticate(data) {
        localStorage.setItem('currentUser', JSON.stringify({ 
             id: data.id,
             userName: data.user_name,
             userRole: data.role,
             email: data.email,
        }));
        localStorage.setItem('userRole', data.role);
    }

    checkIfUserIsAdmin(): boolean {
        return localStorage.getItem('userRole') == "administrator" ? true : false;
    }

    checkIfLoggedIn(): boolean {
        return localStorage.getItem('currentUser') != null ? true : false;         
    }
}