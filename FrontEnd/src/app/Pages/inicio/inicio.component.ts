import { Component, inject } from '@angular/core';

import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { ProductoService } from '../../Services/producto.service';
import { Producto } from '../../Models/Producto';
import { Router } from '@angular/router';


@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatIconModule, MatButtonModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

  private productoServicio = inject(ProductoService);
  public listaProductos: Producto[] =[];
  public displayedColumns : string [] =['nombre', 'descripcion', 'precio', 'cantidad', 'acciones'];

  obtenerProductos(){
    this.productoServicio.get().subscribe({
      next:(data)=> {
        if(data.length>0){
          this.listaProductos = data;
        }
      },
      error:(err)=> {
        console.log(err.message)
      }
    })
  }

  constructor(private router:Router){
      
    this.obtenerProductos();

  }  
  
  nuevo(){
    this.router.navigate(['/producto',0]);
  }

  editar(obj: Producto){
     this.router.navigate(['/producto', obj.idProducto]);
  }

  eliminar(obj: Producto){
    if(confirm("Desea eliminar el producto"+ obj.nombre+"?")){
      this.productoServicio.delete(obj.idProducto).subscribe({
        next:(data)=> {
          if(data.isSuccess){
            this.obtenerProductos();
          }else{
            alert("No se puedo eliminar")
          }
        },
        error:(err)=> {
          console.log(err.message)
        }
      })
    }
  }

}
