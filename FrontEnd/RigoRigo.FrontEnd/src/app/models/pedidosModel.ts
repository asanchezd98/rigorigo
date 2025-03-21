import { DetallePedidoModel } from './detallePedidoModel';

export interface PedidosModel {
  cedula: string;
  nombre: string;
  direccion: string;
  detalles: DetallePedidoModel[];
}