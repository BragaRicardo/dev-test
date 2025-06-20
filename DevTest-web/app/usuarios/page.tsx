'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import axios from 'axios';
import Swal from 'sweetalert2';

type Usuario = {
  id: number;
  nombreCompleto: string;
  correo: string;
  estado: string;
  rol: string;
};

export default function UsuariosPage() {
  const [isCheckingAuth, setIsCheckingAuth] = useState(true);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const router = useRouter();

  useEffect(() => {
    const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;
    setIsAuthenticated(!!token);
    setIsCheckingAuth(false);
  }, []);

  useEffect(() => {
    if (!isCheckingAuth && !isAuthenticated) {
      router.replace('/login');
    }
  }, [isCheckingAuth, isAuthenticated, router]);

  const cargarUsuarios = () => {
    const token = localStorage.getItem('token');
    axios.get(
      `${process.env.NEXT_PUBLIC_API_URL || 'https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net'}/api/usuarios`,
      { headers: { Authorization: `Bearer ${token}` } }
    )
    .then((res) => setUsuarios(res.data))
    .catch(() => router.push('/login'));
  };

  useEffect(() => {
    if (isAuthenticated) {
      cargarUsuarios();
    }
  }, [isAuthenticated]);

  const eliminarUsuario = async (id: number, nombre: string) => {
    const confirm = await Swal.fire({
      icon: 'warning',
      title: '¿Está seguro?',
      text: `¿Desea eliminar al usuario ${nombre}?`,
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
    });

    if (confirm.isConfirmed) {
      const token = localStorage.getItem('token');
      try {
        await axios.delete(
          `${process.env.NEXT_PUBLIC_API_URL || 'https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net'}/api/usuarios/${id}`,
          { headers: { Authorization: `Bearer ${token}` } }
        );
        await Swal.fire('Eliminado', 'El usuario fue eliminado correctamente.', 'success');
        cargarUsuarios();
      } catch {
        await Swal.fire('Error', 'No se pudo eliminar el usuario.', 'error');
      }
    }
  };

  return (
    <main className="min-h-screen bg-gray-100 p-6">
      <div className="max-w-6xl mx-auto bg-white rounded-xl shadow-md p-6">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-bold text-black">Lista de Usuarios</h1>
          <button
            onClick={() => router.push('/usuarios/nuevo')}
            className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            + Agregar
          </button>
        </div>
        <div className="overflow-x-auto">
          <table className="min-w-full text-sm text-left text-black">
            <thead className="bg-gray-200 text-gray-700">
              <tr>
                <th className="px-4 py-2">Nombre</th>
                <th className="px-4 py-2">Correo</th>
                <th className="px-4 py-2">Rol</th>
                <th className="px-4 py-2">Estado</th>
                <th className="px-4 py-2 text-center">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {usuarios.map((usuario) => (
                <tr key={usuario.id} className="border-t">
                  <td className="px-4 py-2">{usuario.nombreCompleto}</td>
                  <td className="px-4 py-2">{usuario.correo}</td>
                  <td className="px-4 py-2">{usuario.rol}</td>
                  <td className="px-4 py-2">{usuario.estado}</td>
                  <td className="px-4 py-2 text-center space-x-2">
                    <button
                      onClick={() => router.push(`/usuarios/editar/${usuario.id}`)}
                      className="bg-yellow-400 text-white px-3 py-1 rounded hover:bg-yellow-500"
                    >
                      Editar
                    </button>
                    <button
                      onClick={() => eliminarUsuario(usuario.id, usuario.nombreCompleto)}
                      className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600"
                    >
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
              {usuarios.length === 0 && (
                <tr>
                  <td colSpan={5} className="text-center py-4 text-gray-500">
                    No se encontraron usuarios.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </main>
  );
}