import { Producto } from './producto';

export interface DetallePedido {
  detallePedidoID?: number;
  pedidoID?: number;
  productoID: number;
  cantidad: number;
  precioUnitario: number;
  producto?: Producto;
}