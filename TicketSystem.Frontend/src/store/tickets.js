import { defineStore } from 'pinia';
import axios from 'axios';
import { API_URL } from './auth';

export const useTicketsStore = defineStore('tickets', {
  state: () => ({
    tickets: [],
    selectedTicket: null,
    comments: [],
    auditLog: [],
    loading: false,
    loadingComments: false,
  }),
  actions: {
    async fetchAll(filters = {}) {
      this.loading = true;
      try {
        const params = { Page: 1, PageSize: 100, ...filters };
        const response = await axios.get(`${API_URL}/tickets`, { params });
        const result = response.data.data;
        this.tickets = (result && result.data) ? result.data : (Array.isArray(result) ? result : []);
        return this.tickets;
      } finally {
        this.loading = false;
      }
    },
    async fetchMine() {
      this.loading = true;
      try {
        const response = await axios.get(`${API_URL}/tickets/mis-tickets`);
        this.tickets = response.data;
        return this.tickets;
      } finally {
        this.loading = false;
      }
    },
    async updateStatus(ticketId, newStatus) {
      await axios.patch(`${API_URL}/tickets/${ticketId}/estado`, newStatus, {
        headers: { 'Content-Type': 'application/json' }
      });
      // Update local ticket status immediately 
      const t = this.tickets.find(t => t.id === ticketId);
      if (t) {
        const statusMap = { 0: 'Pendiente', 1: 'EnProceso', 2: 'Resuelto', 3: 'Cerrado', 4: 'EsperandoUsuario' };
        t.estado = statusMap[newStatus] || newStatus;
        if (this.selectedTicket?.id === ticketId) {
          this.selectedTicket = { ...this.selectedTicket, estado: t.estado };
        }
      }
    },
    async deleteTicket(id) {
      await axios.delete(`${API_URL}/tickets/${id}`);
      this.tickets = this.tickets.filter(t => t.id !== id);
    },
    async createTicket(data) {
      const response = await axios.post(`${API_URL}/tickets`, data);
      return response.data;
    },
    selectTicket(ticket) {
      this.selectedTicket = ticket;
      // Comments are simulated since backend may not have a comment API yet
      this.comments = ticket?._comments || [];
    },
    clearSelection() {
      this.selectedTicket = null;
      this.comments = [];
    },
    addLocalComment(ticketId, comment) {
      if (this.selectedTicket?.id === ticketId) {
        this.comments = [...this.comments, comment];
      }
      // Persist in the ticket object for the session
      const t = this.tickets.find(t => t.id === ticketId);
      if (t) {
        t._comments = [...(t._comments || []), comment];
        if (this.selectedTicket?.id === ticketId) {
          this.selectedTicket = { ...this.selectedTicket, _comments: t._comments };
        }
      }
    }
  },
  getters: {
    byStatus: (state) => (status) => state.tickets.filter(t => t.estado === status),
    stats: (state) => ({
      total: state.tickets.length,
      pending: state.tickets.filter(t => t.estado === 'Pendiente' || t.estado === 0).length,
      inProgress: state.tickets.filter(t => t.estado === 'EnProceso' || t.estado === 1).length,
      resolved: state.tickets.filter(t => t.estado === 'Resuelto' || t.estado === 2).length,
      closed: state.tickets.filter(t => t.estado === 'Cerrado' || t.estado === 3).length,
      waiting: state.tickets.filter(t => t.estado === 'EsperandoUsuario' || t.estado === 4).length,
      urgent: state.tickets.filter(t => t.prioridad === 'Alta' || t.prioridad === 2).length,
    })
  }
});
