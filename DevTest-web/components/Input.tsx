type InputProps = React.InputHTMLAttributes<HTMLInputElement>;

export function Input(props: InputProps) {
  return <input {...props} className="border p-2 w-full" />;
}