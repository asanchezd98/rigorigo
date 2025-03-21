import { DetallePedido } from './detalle-pedido';
import { Cliente } from './cliente';

export interface Pedido {
  pedidoID?: number;
  cliente: Cliente;
  fecha?: Date;
  valorTotal: number;
  detalles: DetallePedido[];
}