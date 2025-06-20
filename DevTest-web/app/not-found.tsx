// app/not-found.tsx
export default function NotFound() {
  return (
    <html lang="es">
      <body>
        <div className="flex min-h-screen items-center justify-center bg-black">
          <div className="text-center">
            <h1 className="text-5xl font-bold text-white mb-4">404</h1>
            <p className="text-lg text-white mb-8">Esta página no existe.</p>
            <a
              href="/login"
              className="px-6 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
            >
              Ir a Inicio de Sesión
            </a>
          </div>
        </div>
      </body>
    </html>
  );
}