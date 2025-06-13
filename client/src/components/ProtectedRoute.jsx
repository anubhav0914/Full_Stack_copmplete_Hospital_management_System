import { Navigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';

const ProtectedRoute = ({ role, children }) => {
  const { user } = useAuth();
  if (!user) return <Navigate to="/login" />;
  console.log(user.role)
  if (role && user.role !== role) return <Navigate to="/unauthorized" />;
  return children;
};

export default ProtectedRoute;
