'use client';

import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/navigation';

type FormData = {
  nombreCompleto: string;
  correo: string;
  password: string;
  estado: string;
  rol: string;
};

export default function NuevoUsuarioPage() {
  const { register, handleSubmit, formState: { errors } } = useForm<FormData>();
  const router = useRouter();

  const onSubmit = async (data: FormData) => {
    const token = localStorage.getItem('token');
    try {
      await axios.post(
        `${process.env.NEXT_PUBLIC_API_URL || 'https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net'}/api/usuarios`,
        data,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      router.push('/usuarios');
    } catch {
      alert('Error al crear el usuario.');
    }
  };

  return (
    <main className="min-h-screen bg-gray-100 flex items-center justify-center p-4">
      <div className="w-full max-w-md bg-white rounded-xl shadow-md p-8">
        <h1 className="text-2xl font-bold text-center text-black mb-6">Nuevo Usuario</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <input
            {...register('nombreCompleto', { required: true })}
            placeholder="Nombre completo"
            className="w-full px-4 py-2 border rounded text-black"
          />
          <input
            {...register('correo', { required: true })}
            placeholder="Correo electrónico"
            className="w-full px-4 py-2 border rounded text-black"
          />
          <input
            {...register('password', { required: true })}
            type="password"
            placeholder="Contraseña"
            className="w-full px-4 py-2 border rounded text-black"
          />
          <select {...register('estado')} className="w-full px-4 py-2 border rounded text-black">
            <option value="ACTIVO">Activo</option>
            <option value="INACTIVO">Inactivo</option>
          </select>
          <select {...register('rol')} className="w-full px-4 py-2 border rounded text-black">
            <option value="ADMIN">Admin</option>
            <option value="CONSULTOR">Consultor</option>
          </select>
          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 font-semibold"
          >
            Guardar
          </button>
        </form>
      </div>
    </main>
  );
}