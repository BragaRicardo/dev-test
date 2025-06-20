'use client';

import { useForm } from 'react-hook-form';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import axios from 'axios';

type LoginForm = {
  correo: string;
  password: string;
};

export default function LoginPage() {
  const { register, handleSubmit } = useForm<LoginForm>();
  const [error, setError] = useState('');
  const router = useRouter();

  const onSubmit = async (data: LoginForm) => {
    try {
      const response = await axios.post(
        `${process.env.NEXT_PUBLIC_API_URL || 'https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net'}/api/auth/login`,
        data
      );
      localStorage.setItem('token', response.data.token);
      router.push('/usuarios');
    } catch {
      setError('Credenciales incorrectas.');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="w-full max-w-md bg-white rounded-2xl shadow-lg p-8 flex flex-col items-center">
        <img
          src="/logo-softec.svg"
          alt="Logo de Softec"
          className="h-20 mb-6"
        />
        <h1 className="text-2xl font-bold text-center text-gray-900 mb-6">Iniciar sesión</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="w-full flex flex-col gap-4">
          <input
            {...register('correo')}
            placeholder="Correo electrónico"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg text-black bg-gray-50 placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            {...register('password')}
            type="password"
            placeholder="Contraseña"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg text-black bg-gray-50 placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          {error && <p className="text-red-600 text-sm">{error}</p>}
          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-3 rounded-lg font-bold hover:bg-blue-700 transition"
          >
            Entrar
          </button>
        </form>
      </div>
    </div>
  );
}