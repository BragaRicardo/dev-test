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
    <main className="min-h-screen bg-gray-100 flex items-center justify-center p-4">
      <div className="w-full max-w-md bg-white rounded-xl shadow-md p-8">
        <div className="flex justify-center mb-6">
          <img
            src="/logo-softec.svg"
            alt="Logo de Softec"
            className="h-14"
          />
        </div>
        <h1 className="text-2xl font-bold text-center text-black mb-6">Iniciar sesión</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <input
            {...register('correo')}
            placeholder="Correo electrónico"
            className="w-full px-4 py-2 border rounded text-black"
          />
          <input
            {...register('password')}
            type="password"
            placeholder="Contraseña"
            className="w-full px-4 py-2 border rounded text-black"
          />
          {error && <p className="text-red-600 text-sm">{error}</p>}
          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition-colors font-semibold"
          >
            Entrar
          </button>
        </form>
      </div>
    </main>
  );
}