import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { Producto } from '../Models/Producto';
import { ResponseAPI } from '../Models/ResponseAPI';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private http = inject(HttpClient);
  private apiUrl: string = appsettings.apiUrl+ "Producto"; 

  constructor() { }

  get(){
    return this.http.get<Producto[]>(this.apiUrl);
  }
  getById(id:number){
    return this.http.get<Producto>(`${this.apiUrl}/${id}`);
  }
  create(obj:Producto){
    return this.http.post<ResponseAPI>(this.apiUrl, obj);
  }
  edit(obj:Producto){
    return this.http.put<ResponseAPI>(this.apiUrl, obj);
  }
  delete(id:number){
    return this.http.delete<ResponseAPI>(`${this.apiUrl}/${id}`);
  }

}
