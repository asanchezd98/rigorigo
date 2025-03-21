import { Component, OnInit } from '@angular/core';
import { PedidoService } from '../../services/pedido.service';
import { ProductoService } from '../../services/producto.service';
import { PedidosModel } from '../../models/PedidosModel';
import { DetallePedido } from '../../models/detalle-pedido';
import { Cliente } from '../../models/cliente';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Producto } from '../../models/producto';

@Component({
  selector: 'app-agregar-pedido',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agregar-pedido.component.html',
  styleUrls: ['./agregar-pedido.component.css'],
})
export class AgregarPedidoComponent implements OnInit {
  pedidoConCliente: PedidosModel = {
    cedula: '',
    nombre: '',
    direccion: '',
    detalles: [],
  };

  productoSeleccionado: DetallePedido = {
    productoID: 0,
    cantidad: 0,
    precioUnitario: 0,
  };

  productos: Producto[] = [];

  constructor(
    private pedidoService: PedidoService,
    private productoService: ProductoService
  ) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (productos) => {
        this.productos = productos;
        console.log('Productos cargados:', productos);
      },
      error: (err) => {
        console.error('Error al cargar los productos:', err);
      },
    });
  }

  agregarProducto(): void {
    const productoID = Number(this.productoSeleccionado.productoID); // Convertir a número
    const producto = this.productos.find((p) => p.productoID === productoID);
  
    if (producto) {
      this.productoSeleccionado.precioUnitario = producto.precio;
      this.pedidoConCliente.detalles.push({ ...this.productoSeleccionado });
      this.productoSeleccionado = { productoID: 0, cantidad: 0, precioUnitario: 0 };
    } else {
      alert('Seleccione un producto válido.');
    }
  }

  guardarPedido(): void {
    // Verifica que los datos del pedido estén completos
    if (
      !this.pedidoConCliente.cedula ||
      !this.pedidoConCliente.nombre ||
      !this.pedidoConCliente.direccion ||
      this.pedidoConCliente.detalles.length === 0
    ) {
      alert('Complete todos los campos del pedido.');
      return;
    }
  
    // Verifica que los detalles del pedido tengan un ProductoID válido
    const detallesInvalidos = this.pedidoConCliente.detalles.some(
      (detalle) => !detalle.productoID || detalle.cantidad <= 0
    );
  
    if (detallesInvalidos) {
      alert('Los detalles del pedido no son válidos.');
      return;
    }
  
    // Enviar el pedido al backend
    this.pedidoService.crearPedidoConCliente(this.pedidoConCliente).subscribe({
      next: (response) => {
        alert('Pedido guardado exitosamente');
        this.pedidoConCliente = {
          cedula: '',
          nombre: '',
          direccion: '',
          detalles: [],
        };
      },
      error: (err) => {
        console.error('Error al guardar el pedido:', err);
      },
    });
  }
}