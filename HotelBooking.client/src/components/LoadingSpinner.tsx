interface LoadingSpinnerProps {
  message?: string;
}

function LoadingSpinner({ message }: LoadingSpinnerProps) {
  return (
    <div className="flex flex-col items-center justify-center py-10">
      <div className="h-8 w-8 animate-spin rounded-full border-4 border-brand-muted border-t-brand-accent" />
      {message && <p className="mt-3 text-sm text-brand-muted">{message}</p>}
    </div>
  );
}

export default LoadingSpinner;
