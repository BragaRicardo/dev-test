import { ReactNode } from 'react';
import Navbar from '../../components/Navbar';

export default function UsuariosLayout({ children }: { children: ReactNode }) {
  return (
    <>
      <Navbar />
      {children}
    </>
  );
}