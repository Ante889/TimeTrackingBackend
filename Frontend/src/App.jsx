import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from 'antd';
import Sidebar from './components/Sidebar';
import HeaderBar from './components/HeaderBar';
import FooterBar from './components/FooterBar';
import ProtectedRoute from './config/ProtectedRoute';
import PublicRoute from './config/PublicRoute';
import Dashboard from './pages/Dashboard';
import Projects from './pages/Projects';
import Login from './pages/Login';
import Register from './pages/Register';
import ProjectDetail from './pages/Project/ProjectDetails';
import PhaseCategory from './pages/Project/PhaseCategory';

const App = () => {
  const [collapsed, setCollapsed] = useState(false);

  return (
      <Layout style={{ minHeight: '100vh' }}>
        <Sidebar collapsed={collapsed} setCollapsed={setCollapsed} />
        <Layout>
          <HeaderBar />
          <Routes>
          <Route
              path="/login"
              element={
                <PublicRoute><Login /></PublicRoute>
              }
            /> 
            <Route
              path="/register"
              element={
                <PublicRoute><Register /></PublicRoute>
              }
            /> 
            <Route
              path="/dashboard"
              element={
                <ProtectedRoute><Dashboard /></ProtectedRoute>
              }
            /> 
            <Route
              path="/projects"
              element={
                <ProtectedRoute><Projects /></ProtectedRoute>
              }
            /> 
            <Route
              path="/project-detail/:id"
              element={
                <ProtectedRoute><ProjectDetail/></ProtectedRoute>
              }
            />
            <Route
              path="/project-detail/category/:projectId/:phaseId"
              element={
                <ProtectedRoute><PhaseCategory/></ProtectedRoute>
              }
            />
          </Routes>
          <FooterBar />
        </Layout>
      </Layout>
  );
};

export default App;
