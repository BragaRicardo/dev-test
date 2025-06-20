// components/NavbarClient.tsx
"use client";
import { useRouter, usePathname } from "next/navigation";

export default function NavbarClient() {
  const router = useRouter();
  const pathname = usePathname();

  // Ocultar navbar en /login
  if (pathname === "/login") return null;

  // Solo mostrar si hay token (puedes mejorar esto con contexto o lógica más compleja si querés)
  if (typeof window !== "undefined" && !localStorage.getItem("token")) {
    return null;
  }

  const navClass = (path: string) =>
    `px-4 py-2 rounded transition font-medium ${
      pathname === path
        ? "bg-blue-600 text-white"
        : "text-gray-700 hover:bg-gray-100"
    }`;

  return (
    <nav className="w-full flex items-center bg-white shadow mb-6 px-6 py-3">
      <button onClick={() => router.push('/usuarios')} className={navClass('/usuarios')}>
        Usuarios
      </button>
      <button onClick={() => router.push('/usuarios/nuevo')} className={navClass('/usuarios/nuevo')}>
        Agregar Usuario
      </button>
      <button
        onClick={() => {
          localStorage.removeItem("token");
          router.push("/login");
        }}
        className="ml-auto px-4 py-2 text-gray-500 hover:text-red-600"
      >
        Cerrar sesión
      </button>
    </nav>
  );
}
