'use client';

import { useForm } from 'react-hook-form';
import axios from 'axios';
import Swal from 'sweetalert2';
import { useRouter } from 'next/navigation';

type FormData = {
  nombreCompleto: string;
  cedula: string;
  sexo: 'MASCULINO' | 'FEMENINO';
  correo: string;
  direccion: string;
  password: string;
  rol: 'ADMIN' | 'CONSULTOR';
};

export default function NuevoUsuarioPage() {
  const { register, handleSubmit, formState: { errors }, reset } = useForm<FormData>();
  const router = useRouter();

  const onSubmit = async (data: FormData) => {
    // Limpiar los campos antes de enviar (trim a cada valor)
    const cleanData = {
      ...data,
      nombreCompleto: data.nombreCompleto.trim(),
      cedula: data.cedula.trim(),
      direccion: data.direccion.trim(),
      correo: data.correo.trim(),
      password: data.password.trim()
    };
    const token = localStorage.getItem('token');
    try {
      await axios.post(
        `${process.env.NEXT_PUBLIC_API_URL || 'https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net'}/api/usuarios`,
        cleanData,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      await Swal.fire({
        icon: 'success',
        title: '¡Usuario creado!',
        text: 'El usuario fue creado correctamente.',
        confirmButtonText: 'Ok',
      });
      reset();
      router.push('/usuarios');
    } catch (err: any) {
      let message = "Error al crear el usuario.";
      if (err?.response?.data?.title) message = err.response.data.title;
      await Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message,
        confirmButtonText: 'Cerrar',
      });
    }
  };

  return (
    <main className="min-h-screen bg-gray-100 flex items-center justify-center p-4">
      <div className="w-full max-w-md bg-white rounded-xl shadow-md p-8">
        <h1 className="text-2xl font-bold text-center text-black mb-6">Nuevo Usuario</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">

          {/* Cédula */}
          <div>
            <input
              {...register('cedula', {
                required: 'La cédula es obligatoria.',
                maxLength: { value: 8, message: 'Máximo 8 caracteres.' },
                validate: value => value.trim() !== '' || 'La cédula no puede ser solo espacios.'
              })}
              placeholder="Cédula"
              className="w-full px-4 py-2 border rounded text-black"
            />
            {errors.cedula && (
              <span className="text-red-600 text-xs">{errors.cedula.message}</span>
            )}
          </div>

          {/* Sexo */}
          <div>
            <select
              {...register('sexo', { required: 'El sexo es obligatorio.' })}
              className="w-full px-4 py-2 border rounded text-black"
              defaultValue=""
            >
              <option value="" disabled>Sexo</option>
              <option value="MASCULINO">Masculino</option>
              <option value="FEMENINO">Femenino</option>
            </select>
            {errors.sexo && (
              <span className="text-red-600 text-xs">{errors.sexo.message}</span>
            )}
          </div>

          {/* Nombre Completo */}
          <div>
            <input
              {...register('nombreCompleto', {
                required: 'El nombre completo es obligatorio.',
                maxLength: { value: 100, message: 'Máximo 100 caracteres.' },
                validate: value => value.trim() !== '' || 'El nombre no puede ser solo espacios.'
              })}
              placeholder="Nombre completo"
              className="w-full px-4 py-2 border rounded text-black"
            />
            {errors.nombreCompleto && (
              <span className="text-red-600 text-xs">{errors.nombreCompleto.message}</span>
            )}
          </div>

          {/* Dirección */}
          <div>
            <input
              {...register('direccion', {
                required: 'La dirección es obligatoria.',
                maxLength: { value: 150, message: 'Máximo 150 caracteres.' },
                validate: value => value.trim() !== '' || 'La dirección no puede ser solo espacios.'
              })}
              placeholder="Dirección"
              className="w-full px-4 py-2 border rounded text-black"
            />
            {errors.direccion && (
              <span className="text-red-600 text-xs">{errors.direccion.message}</span>
            )}
          </div>

          {/* Correo */}
          <div>
            <input
              {...register('correo', {
                required: 'El correo es obligatorio.',
                pattern: { value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/, message: 'Formato de correo inválido.' },
                validate: value => value.trim() !== '' || 'El correo no puede ser solo espacios.'
              })}
              placeholder="Correo electrónico"
              className="w-full px-4 py-2 border rounded text-black"
            />
            {errors.correo && (
              <span className="text-red-600 text-xs">{errors.correo.message}</span>
            )}
          </div>

          {/* Contraseña */}
          <div>
            <input
              {...register('password', {
                required: 'La contraseña es obligatoria.',
                minLength: { value: 8, message: 'Mínimo 8 caracteres.' },
                maxLength: { value: 64, message: 'Máximo 64 caracteres.' },
                validate: value => value.trim() !== '' || 'La contraseña no puede ser solo espacios.'
              })}
              type="password"
              placeholder="Contraseña"
              className="w-full px-4 py-2 border rounded text-black"
            />
            {errors.password && (
              <span className="text-red-600 text-xs">{errors.password.message}</span>
            )}
          </div>

          {/* Rol */}
          <div>
            <select
              {...register('rol', { required: 'El rol es obligatorio.' })}
              className="w-full px-4 py-2 border rounded text-black"
              defaultValue=""
            >
              <option value="" disabled>Rol</option>
              <option value="ADMIN">Admin</option>
              <option value="CONSULTOR">Consultor</option>
            </select>
            {errors.rol && (
              <span className="text-red-600 text-xs">{errors.rol.message}</span>
            )}
          </div>

          {/* Botón */}
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