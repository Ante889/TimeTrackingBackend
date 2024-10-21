import React from 'react';
import { Navigate } from 'react-router-dom';
import Cookies from 'universal-cookie';
import { getCookieToken } from '../service/cookieService';

const ProtectedRoute = ({ children, isAuthenticated }) => {
  const cookies = new Cookies();
  if (!getCookieToken()) {
    return <Navigate to="/login" replace />;
  }
  return children;
};

export default ProtectedRoute;
