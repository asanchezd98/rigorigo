import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido } from '../models/pedido';
import { PedidosModel } from '../models/PedidosModel';

@Injectable({
  providedIn: 'root',
})
export class PedidoService {
  private apiUrl = 'https://localhost:44383/api/pedidos';

  constructor(private http: HttpClient) {}

  crearPedidoConCliente(pedidoConCliente: PedidosModel): Observable<any> {
    return this.http.post(`${this.apiUrl}`, pedidoConCliente);
  }

  obtenerPedidosPorCliente(clienteId: number): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(`${this.apiUrl}/${clienteId}`);
  }
}