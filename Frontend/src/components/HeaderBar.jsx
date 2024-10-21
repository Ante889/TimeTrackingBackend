import React from 'react';
import { Layout, Button, theme } from 'antd';
import { useNavigate } from 'react-router-dom';
import { logoutUser } from '../service/authService';
import { getCookieToken } from '../service/cookieService';

const { Header } = Layout;

const HeaderBar = () => {
  const navigate = useNavigate();
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const token = getCookieToken();
  const userName = localStorage.getItem('userFullName');

  const handleLogout = () => {
    logoutUser(navigate);
  };

  return (
    <Header
      style={{
        padding: '0 16px',
        background: colorBgContainer,
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
      }}
    >
      <h1 style={{ fontSize: '24px' }}>Time tracking application - {userName}</h1>
      {token && (
        <Button type="primary" danger style={{ marginLeft: 'auto' }} onClick={handleLogout}>
          Logout
        </Button>
      )}
    </Header>
  );
};

export default HeaderBar;
