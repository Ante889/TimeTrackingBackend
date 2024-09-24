import React from 'react';
import { Navigate } from 'react-router-dom';

const PublicRoute = ({ children, isAuthenticated }) => {
  if (localStorage.getItem('token')) {
    return <Navigate to="/dashboard" replace />;
  }
  return children;
};

export default PublicRoute;
