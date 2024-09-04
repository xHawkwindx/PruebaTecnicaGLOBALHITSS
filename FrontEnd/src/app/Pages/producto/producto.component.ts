import { Component, inject, Input, OnInit } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule , Validators} from '@angular/forms';
import { ProductoService } from '../../Services/producto.service';
import { Router } from '@angular/router';
import { Producto } from '../../Models/Producto';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, CommonModule],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.css'
})
export class ProductoComponent implements OnInit{

  @Input('id') idProducto! :number;
  private productoServicio = inject(ProductoService);
  public formBuild = inject (FormBuilder);

  public formProducto: FormGroup = this.formBuild.group({
    nombre: ["", [Validators.required, Validators.minLength(3)]],
    descripcion: ["", [Validators.required, Validators.maxLength(150)]],
    precio: ["", [Validators.required, Validators.min(0)]],
    cantidadStock: ["", [Validators.required, Validators.min(1), Validators.max(1000)]]
  });

  constructor(private router: Router){}

  onSubmit() {
    if (this.formProducto.valid) {
      console.log('Formulario válido', this.formProducto.value);
    } else {
      console.log('Formulario inválido');
    }
  }

  ngOnInit(): void {
    if(this.idProducto!=0){
      this.productoServicio.getById(this.idProducto).subscribe({
        next:(data)=>{
          this.formProducto.patchValue({
            nombre: data.nombre,
            descripcion: data.descripcion,
            precio: data.precio,
            cantidadStock: data.cantidadStock
          })
        },
        error:(err)=> {
         console.log(err.message) 
        }
      })
    }
  }

  guardar(){
    const objeto: Producto ={
      idProducto : this.idProducto,
      nombre : this.formProducto.value.nombre,
      descripcion : this.formProducto.value.descripcion,
      precio : this.formProducto.value.precio,
      cantidadStock : this.formProducto.value.cantidadStock 
    }

    if(this.idProducto==0){
      this.productoServicio.create(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al crear");
          }
        },
        error:(err)=> {
         console.log(err.message) 
        }
      })
    }else{
      this.productoServicio.edit(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al editar");
          }
        },
        error:(err)=> {
         console.log(err.message) 
        }
      })
    }

  }

  volver(){
    this.router.navigate(["/"]);
  }

}
